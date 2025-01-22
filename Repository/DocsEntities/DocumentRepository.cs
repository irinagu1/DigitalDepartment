using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Core;
using Repository.Extensions;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
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

        public Task<Document> GetDocumentAsync(int id, bool trackChanges) =>
        FindByCondition(d => d.Id == id, trackChanges).FirstOrDefaultAsync();
        public Document GetDocument(int id, bool trackChanges) =>
          FindByCondition(d => d.Id == id, trackChanges).FirstOrDefault();

        public void CreateDocument(Document document) => Create(document);
        public void UpdateDocument(Document document) => Update(document);


        public PagedList<DocumentShowDto> GetAllDocumentsForShowAsync
            (DocumentShowParameters documentParameters, bool trackChanges)
        {
            var documents = (from d in _repositoryContext.Documents
                             join l in _repositoryContext.Letters
                                 on d.LetterId equals l.Id
                             join u in _repositoryContext.Users
                                 on l.AuthorId equals u.Id
                             where d.isArchived == documentParameters.isArchived
                             select new DocumentShowDto
                             {
                                 Id = d.Id,
                                 Name = d.Name,
                                 isArchived = d.isArchived,
                                 LetterId = l.Id,
                                 LetterCreationDate = l.CreationDate.Value,
                                 LetterAuthorId = l.AuthorId,
                                 LetterAuthorFullName = u.FirstName + " " +
                                 u.SecondName + " " + u.LastName
                             }).Distinct();
            return ApplyParameters(documents, documentParameters);
        }


        private PagedList<DocumentShowDto> ApplyParameters
          (IQueryable<DocumentShowDto> documentShowDto,
          DocumentShowParameters parameters)
        {
            var count = documentShowDto.Count();
            documentShowDto = documentShowDto
              .Search(parameters.SearchByName, parameters.SearchByAuthor)
              .Filter(parameters.CreationDate)
              .Sort()
              .Skip((parameters.PageNumber - 1) * parameters.PageSize)
              .Take(parameters.PageSize);

            var documentsList = AddCanDelete(documentShowDto.ToList());

            return new PagedList<DocumentShowDto>(documentsList,
                                                count,
                                                parameters.PageNumber,
                                                parameters.PageSize);
        }



        private List<DocumentShowDto> AddCanDelete(List<DocumentShowDto> dtos)
        {
            for(int i=0; i< dtos.Count(); i++)
            {
                var isToChecks = (from ch in _repositoryContext.ToChecks
                                 join v in _repositoryContext.DocumentVersions
                                     on ch.VersionId equals v.Id
                                 join d in _repositoryContext.Documents
                                     on v.DocumentId equals d.Id
                                 where d.Id == dtos[i].Id
                                 select new
                                 {
                                     ch.Id
                                 }).ToList();
                if (isToChecks.Count() >=1)
                    dtos[i].CanDelete = false;
                else
                    dtos[i].CanDelete = true;
            }
            return dtos;
        }


        public void DeleteDocument(int documentId)
        {
            var toChecks =  _repositoryContext.ToChecks.Where(ch => ch.Version.DocumentId == documentId);
            _repositoryContext.ToChecks.RemoveRange(toChecks);

            var versions = _repositoryContext.DocumentVersions.Where(v => v.DocumentId == documentId);
            _repositoryContext.DocumentVersions.RemoveRange(versions);

            var document = _repositoryContext.Documents.Where(d => d.Id == documentId);
            _repositoryContext.Documents.RemoveRange(document);

            _repositoryContext.SaveChanges();
        }

        //CHECKED

        public async Task<PagedList<Document>> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges)
        {
            var documents = await FindAll(trackChanges)
                                     .FilterDocuments(documentParameters.Status, documentParameters.Category, documentParameters.CreationDate)
                                     .SearchDocuments(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .SortDocuments(documentParameters.OrderBy)
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
                                     .SearchDocuments(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .SortDocuments(documentParameters.OrderBy)
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
            
            if(documentParameters.IsSigned is not null)
                documentsToList = FilterSigned(documentsToList, documentParameters.IsSigned.Value, userId);

          
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
                                     .SearchDocuments(documentParameters.SearchByName, documentParameters.SearchByAuthor)
                                     .SortDocuments(documentParameters.OrderBy)
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

            if (documentParameters.IsSigned is not null)
                documentsToList = FilterSigned(documentsToList, documentParameters.IsSigned.Value, userId);

            return new PagedList<Document>(documentsToList,
                                 count,
                                 documentParameters.PageNumber,
                                 documentParameters.PageSize);
        }


      

       
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
                         join ch in _repositoryContext.ToChecks on d.Id equals 10/* ch.DocumentId*/
                         where ch.UserId == userId && d.Id == documentId
                         select ch;
            if (entity.FirstOrDefault() is not null)
                return true;
            return false;
        }

    

        public Task<Document> GetDocumentbyPathAsync(string path, bool trackChanges) =>
            FindByCondition(d => /*d.Path == path*/ d.isArchived == false, trackChanges).SingleOrDefaultAsync();
          
        public List<Document> GetDocumentsByLetterId(int letterId) =>
            FindByCondition(d=> d.LetterId == letterId, false).ToList();


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
