using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Shared.RequestFeatures;
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

        public async Task<PagedList<Document>> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges)
        {
            var documents = await FindAll(trackChanges)
                                      .OrderBy(dc => dc.Name)
                                      .Skip((documentParameters.PageNumber - 1) * documentParameters.PageSize)
                                      .Take(documentParameters.PageSize)
                                      .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<Document>(documents,
                                                count,
                                                documentParameters.PageNumber,
                                                documentParameters.PageSize);
        }

        public void CreateDocument(Document document) => Create(document);

        public Task<Document> GetDocumentAsync(int id, bool trackChanges) =>
            FindByCondition(d => d.Id == id, trackChanges).FirstOrDefaultAsync();

        public Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges) =>
            FindByCondition(d => d.Path == path, trackChanges).SingleOrDefaultAsync();
    }
}
