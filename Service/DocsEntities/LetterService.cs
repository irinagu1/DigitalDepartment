using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Service.Contracts.DocsEntities;
using Service.Reports;

//using Service.ReportsManipulation;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using Shared.DataTransferObjects.Roles;
using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class LetterService : ILetterService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _cofigurator;

        public LetterService(IRepositoryManager repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _cofigurator = configuration;
        }

        public async Task<bool> CheckAfterUploadingAndClearIfUncorrect(int letterId)
        {
            bool flag = true;
            var letterEntity=  await _repository.Letter.GetLetterById(letterId);
            if (letterEntity is null)
            {
                await ClearAboutDocuments(letterId);
                return false;
            }

            var documents = _repository.Document.GetDocumentsByLetterId(letterId);
            if(documents.Count == 0)
            {
                await ClearAboutDocuments(letterId);
                return false;
            }

            var baseFolder = returnBaseFolder();
            if (baseFolder == "not found") return false;

            for(int i=0;i<documents.Count; i++)
            {
                var versions = _repository.DocumentVersion.
                    GetAllVersionsByDocumentId(documents[i].Id).ToList();
                if (versions.Count == 0)
                {
                    await ClearAboutDocuments(letterId);
                    return false;
                }
                else
                {
                    for (int j = 0; j < versions.Count; j++)
                    {
                        var path = Path.Combine(baseFolder, versions[i].Path);
                        var isExcist = File.Exists(path);
                        if (!isExcist)
                        {
                            await ClearAboutDocuments(letterId);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private string returnBaseFolder()
        {
            string folder = _cofigurator.GetSection("BaseFolder").AsEnumerable().FirstOrDefault().Value;
            if (folder is null)
                return "not found";
            return folder;
        }

        public async Task ClearAboutDocuments(int letterId) {
            var baseFolder = returnBaseFolder();

            var documents = _repository.Document.GetDocumentsByLetterId(letterId);
            if(documents.Count != 0)
            {
                for (int i = 0; i < documents.Count; i++) {
                    var versions = _repository.DocumentVersion.
                     GetAllVersionsByDocumentId(documents[i].Id).ToList();
                    if (versions.Count != 0) {
                        for(int j = 0; j < versions.Count; j++) 
                        { 
                            var path = Path.Combine(baseFolder, versions[j].Path);
                            var isExcist = File.Exists(path);
                            if (isExcist)
                            {
                                File.Delete(path);
                            }
                        }
                    }
                }
            }
            var letterEntity = await _repository.Letter.GetLetterById(letterId);
            _repository.Letter.DeleteLetter(letterEntity);
            _repository.Save();
        }

        public async Task<LetterDto> CreateLetterAsync(LetterForCreationDto letterForCreationDto)
        {
            var letterEntity = _mapper.Map<Letter>(letterForCreationDto);
            _repository.Letter.CreateLetter(letterEntity);
            await _repository.SaveAsync();
            var letterToReturn = _mapper.Map<LetterDto>(letterEntity);
            return letterToReturn;
        }


        public async Task<string> StoreRecipients(RecipientsForCreationDto recipientsForCreationDto)
        {
            if (recipientsForCreationDto.UsersIds is not null)
            {
                List<int> usRecIds = await StoreData(recipientsForCreationDto.UsersIds,
                    recipientsForCreationDto.LetterId,
                    recipientsForCreationDto.ToCheck,
                    "user");
            }
            if (recipientsForCreationDto.RolesIds is not null)
            {
                List<int> rolesRecIds = await StoreData(recipientsForCreationDto.RolesIds,
                    recipientsForCreationDto.LetterId,
                    recipientsForCreationDto.ToCheck,
                    "role");
            }

            return "success";
        }

        async Task<List<int>> StoreData(string[] data, int letterId, bool ToCheck, string who)
        {
            List<int> toReturn = new List<int>();
            foreach (var rec in data)
            {
                RecipientDto recipientDtoForAdding = new RecipientDto()
                {
                    LetterId = letterId,
                    Type = who,
                    TypeId = rec,
                    ToCheck = ToCheck,

                };
                var recipientEntity = _mapper.Map<Recipient>(recipientDtoForAdding);
                _repository.Recipient.CreateRecipient(recipientEntity);
                toReturn.Add(recipientEntity.Id);
            }
            await _repository.SaveAsync();
            return toReturn;

        }

        public async Task<AllRecipientsByCategoryDto> GetRecipientsByLetterId(int letterId)
        {
           var letterEntity = await _repository.Letter.GetLetterById(letterId);
            if (letterEntity is null)
                throw new LetterNotFoundException(letterId);

            var dtoToReturn = new AllRecipientsByCategoryDto()
            {
                UserToCheck = await GetUsers(letterId, true),
                UserToShow = await GetUsers(letterId, false),
                RolesToCheck = await GetRoles(letterId, true),
                RolesToShow= await GetRoles(letterId, false),
            };

            return dtoToReturn;
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
            var roles = allRolesEntities.Where(r=> rolesIdsToShow.Contains(r.Id));
            var rolesDto =  _mapper.Map<IEnumerable<RolesDto>>(roles);

            return rolesDto;
        }

        public async Task<IEnumerable<RecipientsForReportDto>>
          GetRecipientsForReportByVersionId(long versionId)
        {
            var versionEntity = await _repository.DocumentVersion
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
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = toCheck.DateChecked });
                else
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = null });
            }
            return recipientsForReport;

        }

        public async Task CreateReport(long versionId)
        {
            var version = await _repository.DocumentVersion.GetVersionById(versionId);
            if (version is null || version.Path is null)
                throw new Exception("no such version or no path defined");

            var document = _repository.Document.GetDocument(version.DocumentId, false);
            if (document is null)
                throw new Exception("no such document");

            var recipients = await GetRecipientsForReportByVersionId(versionId);

            var folder = _cofigurator.GetSection("BaseFolderReport").AsEnumerable().FirstOrDefault().Value;
            if (folder is null)
                throw new Exception("Cannot find folder in appsettings");

            ReportGenerator reportGenerator = new ReportGenerator(folder);
            reportGenerator.CreateGeneralReport(recipients, document.Name ?? "", version.Number, version.Path);
        }
    }
}
