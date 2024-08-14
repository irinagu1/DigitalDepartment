using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
       
        private readonly Lazy<IDocumentCategoryRepository>
            _documentCategoryRepository ;
       
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _documentCategoryRepository = 
                new Lazy<IDocumentCategoryRepository>(() => 
                    new DocumentCategoryRepository(repositoryContext));
        }
        
        public IDocumentCategoryRepository DocumentCategory =>
            _documentCategoryRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
    }

}
