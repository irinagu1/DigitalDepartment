using Entities.Models;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IDocumentVersionRepository
    {
        List<VersionDto> AddCanDelete(List<VersionDto> dtos);
        void CreateDocumentVersion(DocumentVersion documentVersion);
        Task<DocumentVersion> GetVersionById(long versionId);
        IEnumerable<DocumentVersion> GetAllVersionsByDocumentId(int versionId);
        Task<DocumentVersion> GetLastVersionOfDocument(int documentId);
        void UpdateDocumentVersion(DocumentVersion documentVersion);
        void DeleteDocumentVersion(long versionId);

        PagedList<VersionShowDto> GetAllVersionsByRolesList(
            HashSet<string> rolesIds,
            VersionParameters parameters
            );
        PagedList<VersionShowDto> GetAllVersionsByUser(
            string userId,
            VersionParameters parameters
            );

        PagedList<VersionShowDto> GetAllVersionsWhereAuthorIsUser(
           string userId,
           VersionParameters parameters
           );

    }
}
