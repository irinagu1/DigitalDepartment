using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.DataTransferObjects.Roles;
using Shared.DataTransferObjects.Users;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    public sealed class DocumentVersionService : IDocumentVersionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checker;
        private readonly IConfiguration _configuration;

        public DocumentVersionService(
            IRepositoryManager repository, 
            IMapper mapper, 
            ICheckerService checker,
            IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _checker = checker;
            _configuration = configuration;
        }

        public async Task<string> NameForDownload(long versionId)
        {
            var version = await _repository.DocumentVersion.GetVersionById(versionId);
            if (version is null)
                throw new Exception("not found version with given id");
            var document = await _repository.Document.GetDocumentAsync(version.DocumentId, false);
            if (document is null)
                throw new Exception("not found document with given id");
            var typeVersion = version.Path.Split(".");
            string name = $"{document.Name} версия {version.Number}.{typeVersion.Last()}";
            return name;
        }
        public async Task<string> returnVersionPath(long versionId)
        {
            var version = await _repository.DocumentVersion.GetVersionById(versionId);
            if (version is null)
                throw new Exception("not found version with given id");
            return version.Path;

        }
        public string returnBaseFolder()
        {
            string folder = _configuration.GetSection("BaseFolder").AsEnumerable().FirstOrDefault().Value;
            if (folder is null)
                throw new Exception("Cannot find folder in appsettings");
            return folder;
        }
        public string returnBaseFolderReport()
        {
            string folder = _configuration.GetSection("BaseFolderReport").AsEnumerable().FirstOrDefault().Value;
            if (folder is null)
                throw new Exception("Cannot find folder in appsettings");
            return folder;
        }

        public async Task<string> DownloadFile(long versionId)
        {
            var version = await _repository.DocumentVersion
                .GetVersionById(versionId);
            if (version is null)
                throw new Exception("No such version");
            string folder = returnBaseFolder();
            
            string filepath = Path.Combine(folder, version.Path);
            if (System.IO.File.Exists(filepath))
                return filepath; 
            else
                return "error";
        }

        public async Task DeleteFileWithVersion(long versionId)
        {
            var version = await _repository.DocumentVersion
                .GetVersionById(versionId);
            if (version is null)
                throw new Exception("No such version");
            try
            {
                string baseFolder = returnBaseFolder();
                string fullPath = Path.Combine(baseFolder, version.Path);
                System.IO.File.Delete(fullPath);
            }
            catch (Exception ex) { throw new Exception("cannot delete file in disk"); }
        }

        public async Task<IEnumerable<RecipientsForReportDto>> 
            GetRecipientsForReportByVersionIs(long versionId)
        {
            var versionEntity= await _repository.DocumentVersion
                .GetVersionById(versionId);
            if (versionEntity is null)
                throw new Exception("no such version");
            var letterEntity = await _repository.Letter
                .GetLetterByDocumentId(versionEntity.DocumentId);
            if (letterEntity is null)
                throw new Exception("no such letter");

            var usersDto = await GetUsers(letterEntity.Id, true);
            var rolesDto = await GetRoles(letterEntity.Id, true);

            foreach (var role in rolesDto)
            {
                var usersByRole = await _repository.User.GetUsersByRoleId(role.Id);
                var dto = _mapper.Map<IEnumerable<UserDto>>(usersByRole);
                usersDto = usersDto.Concat(dto);
            }

            usersDto = usersDto.DistinctBy(u => u.Id).ToList();

            var recipientsForReport = new List<RecipientsForReportDto>();
            foreach (var user in usersDto)
            {
                if (user.PositionId is not null)
                {
                    var position = await _repository.Position.GetPositionByIdAsync(user.PositionId.Value, false);
                    user.PositionName = position.Name;
                }

                var toCheck = await _repository.ToCheck
                    .GetToCheckByUserAndVersionId(user.Id, versionId);
                if (toCheck is not null)
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = toCheck.DateChecked});
                else
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = null });
            }
            return recipientsForReport;

        }


        private async Task<IEnumerable<UserDto>> GetUsers(int letterId, bool isToCheck)
        {
            var allUserEntities = await _repository.User.GetAll();

            var recipientsUsersToShow = await _repository.Recipient.GetRecipientsByTypeAndLetterId("user", letterId, isToCheck);
            var usersIdsToShow = recipientsUsersToShow.Select(u => u.TypeId).ToList();
            var users = allUserEntities.Where(e => usersIdsToShow.Contains(e.Id));

         
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);



            return usersDto;
        }

        private async Task<IEnumerable<RolesDto>> GetRoles(int letterId, bool isToCheck)
        {
            var allRolesEntities = await _repository.Role.GetAllRoles();

            var recipientsRolesToShow = await _repository.Recipient.GetRecipientsByTypeAndLetterId("role", letterId, isToCheck);
            var rolesIdsToShow = recipientsRolesToShow.Select(u => u.TypeId).ToList();
            var roles = allRolesEntities.Where(r => rolesIdsToShow.Contains(r.Id));
            var rolesDto = _mapper.Map<IEnumerable<RolesDto>>(roles);

            return rolesDto;
        }
      
        public async Task<VersionDto> GetVersionById(long versionId)
        {
            var versionEntity = await
                _checker.GetDocumentVersionEntityAndCheckIfExistsAsync(versionId);
            var versionDto = _mapper.Map<VersionDto>(versionEntity);
            var author = _checker.
                GetUserEntityAndCheckItExists(versionDto.AuthorId, false);
            versionDto.AuthorName = author.FirstName + " " + author.SecondName +
                " " + author.LastName;
            return versionDto;
        }

        public async Task DeleteDocumentVersion(long versionId)
        {
            await DeleteFileWithVersion(versionId);
            _repository.DocumentVersion.DeleteDocumentVersion(versionId);
        }


        public async Task CreateAnotherVersionEntity(int documentId, string path, string message, string authorId)
        {
            //find last version entity
            var lastVersionEntity = await 
                _repository.DocumentVersion.GetLastVersionOfDocument(documentId);
            if (lastVersionEntity is null)
                throw new Exception("cannot found last version of document");
          
            //change on false and get number
            var lastVersionNumber = Convert.ToInt32(lastVersionEntity.Number) + 1;
            lastVersionEntity.isLast = false;
            _repository.DocumentVersion.UpdateDocumentVersion(lastVersionEntity);
            _repository.Save();

            //create new enity
            var newEntity = GetVersionForCreationDto(
                lastVersionNumber,
                documentId,
                path,
                message,
                authorId
                );
            var versionEntity = _mapper.Map<DocumentVersion>(newEntity);
            _repository.DocumentVersion.CreateDocumentVersion(versionEntity);

            await _repository.SaveAsync();
        }

        public async Task CreateVersionEntity(int number, string path, 
            string message, DocumentDto documentDto, string authorId)
        {
            _checker.CheckIfPathIsEmpty(path);
            var versionDto = GetVersionForCreationDto(
                number,
                documentDto.Id,
                path,
                message,
                authorId
                );

            var versionEntity = _mapper.Map<DocumentVersion>(versionDto);
            _repository.DocumentVersion.CreateDocumentVersion(versionEntity);

            await _repository.SaveAsync();
        }



        public DocumentVersionForCreationDto GetVersionForCreationDto(
            int number,
            int documentId,
            string path,
            string message,
            string authorId
            )
        {
            var dto = new DocumentVersionForCreationDto()
            {
                Number = number,
                isLast = true,
                DocumentId = documentId,
                Path = path,
                CreationDate = DateTime.Now,
                Message = message,
                AuthorId = authorId
            };
            return dto;
        }

        public async Task<List<VersionDto>>
            GetAllVersionsByDocumentId(int documentId)
        {
            await _checker.GetDocumentEntityAndCheckIfExistsAsync(documentId);
            var versionsEntities = _repository.DocumentVersion.
                GetAllVersionsByDocumentId(documentId);
            var versionsDtos = 
                _mapper.Map<IEnumerable<VersionDto>>(versionsEntities).ToList();
            for (int i = 0; i < versionsDtos.Count(); i++)
            {
                var author = _checker.
                    GetUserEntityAndCheckItExists(versionsDtos[i].AuthorId, false);
                versionsDtos[i].AuthorName = author.FirstName + " " +
                    author.SecondName + " " + author.LastName;
            }

            var withCanDelete = _repository.DocumentVersion.AddCanDelete(versionsDtos);
            return withCanDelete;
        }


        public async  Task<(List<VersionShowDto> versions, MetaData metaData)>
            GetAllVersionsByUser
            (string userId, VersionParameters versionParameters)
        {
            var versionsWithMetaData = _repository.DocumentVersion.
                GetAllVersionsByUser(userId, versionParameters);
            var versionsWithIsChecked = await AddIsCheckedByUser(versionsWithMetaData.ToList(), userId);
            return (versionsWithIsChecked, versionsWithMetaData.MetaData);
        }

        public async Task<(List<VersionShowDto> versions, MetaData metaData)> 
            GetAllVersionsByRoles
            (string userId, VersionParameters versionParameters)
        {
            HashSet<string> uniqueRoles = await _repository.User.GetUserRolesIds(userId);
            var versions = await GetAllVersionsByRolesList(
                uniqueRoles,
                versionParameters,
                userId);
            return versions;
        }
        public async Task<(List<VersionShowDto> versions, MetaData metaData)>
            GetAllVersionsByRolesList(
            HashSet<string> rolesIds, VersionParameters versionParameters, string userId)
        {
            var versionsWithMetaData = _repository.DocumentVersion.
                GetAllVersionsByRolesList(rolesIds, versionParameters);
            var versionsWithIsChecked = await AddIsCheckedByUser(versionsWithMetaData.ToList(), userId);
            return (versionsWithIsChecked, versionsWithMetaData.MetaData);
        }

        public async Task<List<VersionShowDto>> AddIsCheckedByUser(List<VersionShowDto> versions,
            string userId)
        {
            for(int i=0; i< versions.Count; i++)
            {
                var isChecked = await _repository.ToCheck.GetToCheckByUserAndVersionId
                    (userId, versions[i].versionId);
                if(isChecked is not null)
                    versions[i].isChecked = true;
                else versions[i].isChecked = false;
            }
            return versions;
        }

        public (List<VersionShowDto> versions, MetaData metaData)
            GetAllVersionsWhereAuthorIsUser
            (string userId, VersionParameters versionParameters)
        {
            var versionsWithMetaData = _repository.DocumentVersion.
              GetAllVersionsWhereAuthorIsUser(userId, versionParameters);
            return (versionsWithMetaData, versionsWithMetaData.MetaData);
        }
    }
}
