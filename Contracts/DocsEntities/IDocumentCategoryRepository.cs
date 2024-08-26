using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IDocumentCategoryRepository
    {
        Task<PagedList<DocumentCategory>> GetAllDocumentCategoriesAsync(
            DocumentCategoryParameters documentCategoryParameters, bool trackChanges);
        //get one by id
        Task<DocumentCategory> GetDocumentCategoryAsync(int documentCategoryId, bool trackChanges);

        //create new
        void CreateDocumentCategory(DocumentCategory documentCategory);

        //delete
        void DeleteDocumentCategory(DocumentCategory documentCategory);
    }
}
