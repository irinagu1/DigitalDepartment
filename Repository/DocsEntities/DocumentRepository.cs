using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Extensions;
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
                                     .FilterDocuments(documentParameters.Status, documentParameters.Category, documentParameters.CreationDate)
                                     .Search(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .Sort(documentParameters.OrderBy)
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
                           .FilterDocuments(documentParameters.Status, documentParameters.Category, documentParameters.CreationDate)
                                     .Search(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .Sort(documentParameters.OrderBy)
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

            List<Document> documentsToList = new List<Document>();
            foreach (var item in documents)
            {
                documentsToList.Add(item.d);
            }
            
            if(documentParameters.IsSigned is not null)
                documentsToList = FilterSigned(documentsToList, documentParameters.IsSigned.Value, userId);

            var count = await documents.CountAsync();
            documents = documents.Skip((documentParameters.PageNumber - 1) * documentParameters.PageSize)
                                      .Take(documentParameters.PageSize);
      
            return new PagedList<Document>(documentsToList,
                                             count,
                                             documentParameters.PageNumber,
                                             documentParameters.PageSize);
        }

        public async Task<PagedList<Document>> GetAllDocumentsForRolesAsync
           (string userId, HashSet<string> rolesIds, bool toCheck, DocumentParameters documentParameters, bool trackChanges)
        {
            var documents = from d in _repositoryContext.Documents
                              .FilterDocuments(documentParameters.Status, documentParameters.Category, documentParameters.CreationDate)
                                     .Search(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .Sort(documentParameters.OrderBy)
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

            List<Document> documentsToList = new List<Document>();
            foreach (var item in documents)
            {
                documentsToList.Add(item.d);
            }

            if (documentParameters.IsSigned is not null)
                documentsToList = FilterSigned(documentsToList, documentParameters.IsSigned.Value, userId);

            var count = await documents.CountAsync();
            documents = documents.Skip((documentParameters.PageNumber - 1) * documentParameters.PageSize)
                          .Take(documentParameters.PageSize);
         
            return new PagedList<Document>(documentsToList,
                                 count,
                                 documentParameters.PageNumber,
                                 documentParameters.PageSize);
        }


        public void CreateDocument(Document document) => Create(document);
        public void UpdateDocument(Document document) => Update(document);

       
        public List<Document> FilterSigned(List<Document> documents, bool isSigned, string userId)
        {
            List<Document> newList = new List<Document>();
            foreach (var doc in documents)
            {
                var check = CheckIfDocumentSigned(userId, doc.Id);
                if (isSigned && check)
                    newList.Add(doc);
                else if (!isSigned && !check)
                    newList.Add(doc);
            }
            return newList;
        }

     
        public bool CheckIfDocumentSigned(string userId, int documentId)
        {
            var entity = from d in _repositoryContext.Documents
                         join ch in _repositoryContext.ToChecks on d.Id equals ch.DocumentId
                         where ch.UserId == userId && d.Id == documentId
                         select ch;
            if (entity.FirstOrDefault() is not null)
                return true;
            return false;
        }

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

        public int AmountOfConnectedDocumentsByStatusId(int statusId)
        {
            var count = FindByCondition(d => d.DocumentStatusId == statusId, false).Count();
            return count;
        }

   
    }
}
