using Contracts;
using Entities.Models;

namespace Repository
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
