using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        public DocumentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateDocument(Document document) => Create(document);

        public Task<Document> GetDocumentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges) =>
            FindByCondition(d => d.Path == path, trackChanges).SingleOrDefaultAsync();
    }
}
