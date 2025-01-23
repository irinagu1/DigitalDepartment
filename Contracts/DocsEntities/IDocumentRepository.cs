using Entities.Models;
using Shared.DataTransferObjects.Documents;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IDocumentRepository
    {
        List<Document> GetDocumentsByLetterId(int letterId);
        PagedList<DocumentShowDto> GetAllDocumentsForShowAsync
              (DocumentShowParameters documentParameters, bool trackChanges);

        void CreateDocument(Document document);

        void UpdateDocument(Document document);
        void DeleteDocument(int documentId);

        Task<PagedList<Entities.Models.Document>> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges);
        //get one
        Task<Document> GetDocumentAsync(int id, bool trackChanges);
        Document GetDocument(int id, bool trackChanges);
        Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges);
    
        int AmountOfConnectedDocumentsByCategoryId(int categoryId);
        int AmountOfConnectedDocumentsByStatusId(int statusId);

    }
}
