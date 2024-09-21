using Entities.Models;
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
        //get all with filtering
        Task<PagedList<Entities.Models.Document>> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges);
        //get one
        Task<Document> GetDocumentAsync(int id, bool trackChanges);
        Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges);
        //create one
        void CreateDocument(Document document);

        //create collection
        //delete document
    }
}
