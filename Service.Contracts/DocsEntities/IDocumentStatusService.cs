using Entities.Models;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentStatusService
    {
        Task<(IEnumerable<DocumentStatusDto> documentStatuses, MetaData metaData)> 
            GetAllDocumentStatusesAsync(
                DocumentStatusParameters documentStatusParameters, 
                bool trackChanges);

        Task<DocumentStatusDto> GetDocumentStatusAsync(int Id,  bool trackChanges);

        Task<DocumentStatusDto> CreateDocumentStatusAsync(DocumentStatusForCreationDto documentStatus);

        Task DeleteDocumentStatusAsync(int documentStatusId, bool trackChanges);

        Task UpdateDocumentStatusAsync(int documentStatusId, DocumentStatusForUpdateDto documentStatusForUpdate, bool trackChanges);


    }
}
