using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentService
    {
        Task<(IEnumerable<DocumentDto> documents, MetaData metaData)>
           GetAllDocumentsAsync(
               DocumentParameters documentParameters,
               bool trackChanges);


        Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)>
            GetDocumentsForShowAsync(string userId, DocumentParameters documentParameters, bool trackChanges);

        Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)>
            GetAllDocumentsWithParametersNamesAsync(IEnumerable<DocumentDto> documents, MetaData metaData, string userId);

        DocumentDto ArchiveDocument(int id);

        Task<DocumentDto> GetDocumentByPathAsync(string path, bool trackChanges);
        Task<DocumentDto> GetDocumentByIdAsync(int id, bool trackChanges);

        Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto);
        DocumentDto UpdateDocument(DocumentForUpdateDto documentForUpdateDto);
        int AmountOfConnectedDocumentsByCategoryId(int categoryId);

    }
}
