using Contracts.DocsEntities;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Extensions;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentVersionRepository :
        RepositoryBase<DocumentVersion>, IDocumentVersionRepository
    {
        private RepositoryContext _repositoryContext;


        public DocumentVersionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<DocumentVersion> GetVersionById(long versionId)
        {
            var version = await FindByCondition(v => v.Id == versionId, false).
                SingleOrDefaultAsync();
            return version;
        }

        public async Task<DocumentVersion> GetLastVersionOfDocument(int documentId)
        {
            var version =await  FindByCondition(v => v.DocumentId == documentId &&
            v.isLast == true, false).FirstOrDefaultAsync();
            return version;
        }

        public void CreateDocumentVersion(DocumentVersion documentVersion) =>
                Create(documentVersion);

        public void DeleteDocumentVersion(long versionId)
        {
            var version = _repositoryContext.DocumentVersions.Where(v => v.Id == versionId).FirstOrDefault();
            if (version is  null)
                throw new Exception("version not found");
            _repositoryContext.DocumentVersions.Remove(version);
            _repositoryContext.SaveChanges();
            SetLastVersion(version.DocumentId);
        }

        private void SetLastVersion(int documentId)
        {
            var versions = _repositoryContext.DocumentVersions.
                Where(v => v.DocumentId == documentId).
                OrderByDescending(v => v.Number);
            if (versions.Count()>=1)
            {
                var version = versions.First();
                version.isLast = true;
                _repositoryContext.DocumentVersions.Update(version);
                _repositoryContext.SaveChanges();
            }
        }

        public IEnumerable<DocumentVersion> 
            GetAllVersionsByDocumentId(int versionId)
        {
            var versions = FindByCondition(v => v.DocumentId == versionId, false).ToList();
            return versions;

        }

        public void UpdateDocumentVersion(DocumentVersion documentVersion) =>
            Update(documentVersion);

        public  PagedList<VersionShowDto>
            GetAllVersionsByRolesList
            (HashSet<string> rolesIds, VersionParameters parameters)
        {
            var versions = from v in _repositoryContext.DocumentVersions
                           join u in _repositoryContext.Users
                               on v.AuthorId equals u.Id
                           join d in _repositoryContext.Documents
                               on v.DocumentId equals d.Id
                           join cat in _repositoryContext.DocumentCategories
                               on d.DocumentCategoryId equals cat.Id
                           join stat in _repositoryContext.DocumentStatuses
                               on d.DocumentStatusId equals stat.Id
                           join l in _repositoryContext.Letters
                               on d.LetterId equals l.Id
                           join r in _repositoryContext.Recipients
                               on l.Id equals r.LetterId
                           where
                               r.Type == "role" &&
                               rolesIds.Contains(r.TypeId) &&
                               r.ToCheck == parameters.ToCheck
                           select new VersionShowDto
                           {
                               versionId = v.Id,
                               numberVersion = (int)v.Number,
                               versionCreationDate = v.CreationDate.Value,
                               versionMessage = v.Message,
                               versionAuthorId = v.AuthorId,
                               versionAuthorFullName =
                                u.FirstName + " " + u.SecondName + " " + u.LastName,

                               documentId = d.Id,
                               documentName = d.Name,
                               documentStatusId = stat.Id,
                               documentStatusName = stat.Name,
                               documentCategoryId = cat.Id,
                               documentCategoryName = cat.Name,
                               letterId = d.LetterId,
                               toCheck = parameters.ToCheck
                           };
            return ApplyParameters(versions, parameters);
        }

        public PagedList<VersionShowDto>
            GetAllVersionsByUser
            (string userId, VersionParameters parameters)
        {
            var versions = from v in _repositoryContext.DocumentVersions
                            join u in _repositoryContext.Users
                                on v.AuthorId equals u.Id
                            join d in _repositoryContext.Documents
                                on v.DocumentId equals d.Id
                            join cat in _repositoryContext.DocumentCategories
                                on d.DocumentCategoryId equals cat.Id
                            join stat in _repositoryContext.DocumentStatuses
                                on d.DocumentStatusId equals stat.Id
                            join l in _repositoryContext.Letters
                                on d.LetterId equals l.Id
                            join r in _repositoryContext.Recipients
                                on l.Id equals r.LetterId
                            where
                                r.Type == "user" &&
                                r.TypeId == userId &&
                                r.ToCheck == parameters.ToCheck
                           select new VersionShowDto {
                                versionId = v.Id,
                                numberVersion = (int)v.Number,
                                versionCreationDate = v.CreationDate.Value,
                                versionMessage = v.Message,
                                versionAuthorId = v.AuthorId,
                                versionAuthorFullName =
                                 u.FirstName + " " + u.SecondName + " " + u.LastName,

                                documentId = d.Id,
                                documentName = d.Name,
                                documentStatusId = stat.Id,
                                documentStatusName = stat.Name,
                                documentCategoryId = cat.Id,
                                documentCategoryName = cat.Name,
                                letterId = d.LetterId,
                                toCheck = parameters.ToCheck
                            };

            return ApplyParameters(versions, parameters);
        }

        private PagedList<VersionShowDto> ApplyParameters 
            (IQueryable<VersionShowDto> versionShowDto,
            VersionParameters parameters)
        {
            var count = versionShowDto.Count();
            versionShowDto = versionShowDto
              .SearchVersion(parameters.SearchByName, parameters.SearchByAuthor)
              .FilterVersion(parameters.Status, parameters.Category,
                  parameters.CreationDate)
              .Sort()
              .Skip((parameters.PageNumber - 1) * parameters.PageSize)
              .Take(parameters.PageSize);
           
            return new PagedList<VersionShowDto>(versionShowDto.ToList(),
                                                count,
                                                parameters.PageNumber,
                                                parameters.PageSize);
        }
        
        public List<VersionDto> AddCanDelete(List<VersionDto> dtos)
        {
            for(int i=0; i< dtos.Count(); i++)
            {
                var toChecks = _repositoryContext.ToChecks
                    .Where(ch => ch.VersionId == dtos[i].Id).FirstOrDefault();
                if (toChecks is not null)
                    dtos[i].canDelete = false;
                else dtos[i].canDelete = true;
            }
            return dtos;
        }


        public PagedList<VersionShowDto> 
            GetAllVersionsWhereAuthorIsUser
            (string userId, VersionParameters parameters)
        {
            var versions = (from v in _repositoryContext.DocumentVersions
                           join u in _repositoryContext.Users
                               on v.AuthorId equals u.Id
                           join d in _repositoryContext.Documents
                               on v.DocumentId equals d.Id
                           join cat in _repositoryContext.DocumentCategories
                               on d.DocumentCategoryId equals cat.Id
                           join stat in _repositoryContext.DocumentStatuses
                               on d.DocumentStatusId equals stat.Id
                           join l in _repositoryContext.Letters
                               on d.LetterId equals l.Id
                           join r in _repositoryContext.Recipients
                               on l.Id equals r.LetterId
                           where
                              v.AuthorId == userId
                           select new VersionShowDto
                           {
                               versionId = v.Id,
                               numberVersion = (int)v.Number,
                               versionCreationDate = v.CreationDate.Value,
                               versionMessage = v.Message,
                               versionAuthorId = v.AuthorId,
                               versionAuthorFullName =
                                 u.FirstName + " " + u.SecondName + " " + u.LastName,
                               documentId = d.Id,
                               documentName = d.Name,
                               documentStatusId = stat.Id,
                               documentStatusName = stat.Name,
                               documentCategoryId = cat.Id,
                               documentCategoryName = cat.Name,
                               letterId = d.LetterId,
                               toCheck = false
                           }).Distinct();

            return ApplyParameters(versions, parameters);

        }
    }
     
}
