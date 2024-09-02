using Entities.Models;
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
        //get one
        Task<Document> GetDocumentAsync(int id);
        Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges);
        //create one
        void CreateDocument(Document document);

        //create collection
        //delete document
    }
}
