using Shared.DataTransferObjects.DocumentCategories;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentCategoryService
    {

        Task<(IEnumerable<DocumentCategoryDto> documentCategories, MetaData metaData)>
            GetAllDocumentCategoriesAsync(
                DocumentCategoryParameters documentCategoryParameters,
                bool trackChanges);

        Task<DocumentCategoryDto> GetDocumentCategoryAsync(int Id, bool trackChanges);

        Task<DocumentCategoryDto> CreateDocumentCategoryAsync(DocumentCategoryForCreationDto documentCategory);

        Task DeleteDocumentCategoryAsync(int documentCategoryId, bool trackChanges);

        Task UpdateDocumentCategoryAsync(int documentCategoryId, DocumentCategoryForUpdateDto documentCategoryForUpdate, bool trackChanges);
    }
}
