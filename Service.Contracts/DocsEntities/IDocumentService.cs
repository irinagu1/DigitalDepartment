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

        (List<DocumentShowDto> documents, MetaData metaData)
            GetAllDocumentsForShowAsync
         (DocumentShowParameters documentParameters, bool trackChanges);

        Task<bool> DeleteDocument(int documentId);

       DocumentDto ArchiveDocument(int id);


        //CHECKED

        Task<(IEnumerable<DocumentDto> documents, MetaData metaData)>
           GetAllDocumentsAsync(
               DocumentParameters documentParameters,
               bool trackChanges);


        Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)>
            GetDocumentsForShowAsync(string userId, DocumentParameters documentParameters, bool trackChanges);

        Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)>
            GetAllDocumentsWithParametersNamesAsync(IEnumerable<DocumentDto> documents, MetaData metaData, string userId);

     

        Task<DocumentDto> GetDocumentByPathAsync(string path, bool trackChanges);
        Task<DocumentDto> GetDocumentByIdAsync(int id, bool trackChanges);
        Task<DocumentForVersion> GetDocumentByIdForVersion(int id);

        Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto, string authorId);
        DocumentDto UpdateDocument(DocumentForUpdateDto documentForUpdateDto);
        int AmountOfConnectedDocumentsByCategoryId(int categoryId);
        

    }
}
