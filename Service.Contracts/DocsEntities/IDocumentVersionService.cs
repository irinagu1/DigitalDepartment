using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentVersionService
    {
        Task DeleteFileWithVersion(long versionId);
        Task<string> returnVersionPath(long versionId);
        Task<string> NameForDownload(long versionId);
        string returnBaseFolderReport();
        string returnBaseFolder();
        Task<string> DownloadFile(long versionId);
        Task<IEnumerable<RecipientsForReportDto>> 
            GetRecipientsForReportByVersionIs(long versionId);
        Task DeleteDocumentVersion(long versionId);
        Task<VersionDto> GetVersionById(long versionId);
        DocumentVersionForCreationDto GetVersionForCreationDto(
            int number,
            int documentId,
            string path,
            string message,
            string authorId
            );
        Task CreateVersionEntity(int number, string path,
            string message, DocumentDto documentDto, string authorId);
        Task CreateAnotherVersionEntity(int documentId, string path,
         string message, string authorId);

        Task<List<VersionDto>> GetAllVersionsByDocumentId(int documentId);

        Task<(List<VersionShowDto> versions, MetaData metaData)>
            GetAllVersionsByUser(
           string userId,
           VersionParameters versionParameters
           );

        Task <(List<VersionShowDto> versions, MetaData metaData)> 
            GetAllVersionsByRoles(
        string userId,
        VersionParameters versionParameters
        );

        (List<VersionShowDto> versions, MetaData metaData)
           GetAllVersionsWhereAuthorIsUser(
          string userId,
          VersionParameters versionParameters
          );
    }
}
