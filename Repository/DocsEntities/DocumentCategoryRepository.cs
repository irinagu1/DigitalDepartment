using Contracts.DocsEntities;
using Entities.Models;
using Repository.Core;

namespace Repository.DocsEntities
{
    public class DocumentCategoryRepository
        : RepositoryBase<DocumentCategory>, IDocumentCategoryRepository
    {
        public DocumentCategoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
