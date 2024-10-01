using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
    {
        private RepositoryContext _repositoryContext;
        public DocumentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
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

        public async Task<PagedList<Document>> GetAllDocumentsForUserAsync
            (string userId, bool toCheck, DocumentParameters documentParameters, bool trackChanges)
        {
            var documents = from d in _repositoryContext.Documents
                            join l in _repositoryContext.Letters on d.LetterId equals l.Id
                            join r in _repositoryContext.Recipients on l.Id equals r.LetterId
                            where d.isArchived == false
                                  && r.Type == "user"
                                  && r.TypeId == userId
                                  && r.ToCheck == toCheck
                            select new
                            {
                                d
                            };
            var count = await documents.CountAsync();
            documents = documents.Skip((documentParameters.PageNumber - 1) * documentParameters.PageSize)
                                      .Take(documentParameters.PageSize);
            List<Document> documentsToList = new List<Document>();
            foreach (var item in documents)
            {
                documentsToList.Add(item.d);
            }
            return new PagedList<Document>(documentsToList,
                                             count,
                                             documentParameters.PageNumber,
                                             documentParameters.PageSize);
        }

        public async Task<PagedList<Document>> GetAllDocumentsForRolesAsync
           (HashSet<string> rolesIds, bool toCheck, DocumentParameters documentParameters, bool trackChanges)
        {
            var documents = from d in _repositoryContext.Documents
                            join l in _repositoryContext.Letters on d.LetterId equals l.Id
                            join r in _repositoryContext.Recipients on l.Id equals r.LetterId
                            where d.isArchived == false
                                  && r.Type == "role"
                                  && rolesIds.Contains(r.TypeId)
                                  && r.ToCheck == toCheck
                            select new
                            {
                                d
                            };
            var count = await documents.CountAsync();
            documents = documents.Skip((documentParameters.PageNumber - 1) * documentParameters.PageSize)
                          .Take(documentParameters.PageSize);
            List<Document> documentsToList = new List<Document>();
            foreach (var item in documents)
            {
                documentsToList.Add(item.d);
            }
            return new PagedList<Document>(documentsToList,
                                 count,
                                 documentParameters.PageNumber,
                                 documentParameters.PageSize);
        }


        public void CreateDocument(Document document) => Create(document);
        public void UpdateDocument(Document document) => Update(document);

        public Task<Document> GetDocumentAsync(int id, bool trackChanges) =>
            FindByCondition(d => d.Id == id, trackChanges).FirstOrDefaultAsync();
        public Document GetDocument(int id, bool trackChanges) =>
          FindByCondition(d => d.Id == id, trackChanges).FirstOrDefault();

        public Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges) =>
            FindByCondition(d => d.Path == path, trackChanges).SingleOrDefaultAsync();

        public int AmountOfConnectedDocumentsByCategoryId(int categoryId)
        {
            var count = FindByCondition(d=>d.DocumentCategoryId == categoryId, false).Count();
            return count;
        }
    }
}
