using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Repository.Core;
using Shared.RequestFeatures;

namespace Repository.DocsEntities
{
    public class DocumentCategoryRepository
        : RepositoryBase<DocumentCategory>, IDocumentCategoryRepository
    {
        public DocumentCategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        //get all
        public async Task<PagedList<DocumentCategory>> GetAllDocumentCategoriesAsync(
            DocumentCategoryParameters documentCategoryParameters, bool trackChanges)
        {
            var documentCategories = await FindAll(trackChanges)
                                     .OrderBy(dc => dc.Name)
                                     .Skip((documentCategoryParameters.PageNumber - 1) * documentCategoryParameters.PageSize)
                                     .Take(documentCategoryParameters.PageSize)
                                     .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<DocumentCategory>(documentCategories,
                                                count,
                                                documentCategoryParameters.PageNumber,
                                                documentCategoryParameters.PageSize);
        }

        //get one
        public async Task<DocumentCategory> GetDocumentCategoryAsync(int documentCategoryId, bool trackChanges)
            => await FindByCondition(dc => dc.Id.Equals(documentCategoryId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateDocumentCategory(DocumentCategory documentCategory) => Create(documentCategory);

        public void DeleteDocumentCategory(DocumentCategory documentCategory) => Delete(documentCategory);
    }
}
