using Contracts.DocsEntities;
using Contracts.RepositoryCore;
using Repository.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private readonly Lazy<IDocumentCategoryRepository> _documentCategoryRepository;
        private readonly Lazy<IDocumentStatusRepository> _documentStatusRepository;
        private readonly Lazy<IDocumentRepository> _documentRepository;
        private readonly Lazy<ILetterRepository> _letterRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;

            _documentCategoryRepository = new Lazy<IDocumentCategoryRepository>(() =>
            new DocumentCategoryRepository(repositoryContext));
            
            _documentStatusRepository = new Lazy<IDocumentStatusRepository>(() =>
            new DocumentStatusRepository(repositoryContext));

            _documentRepository = new Lazy<IDocumentRepository>(() =>
            new DocumentRepository(repositoryContext));

            _letterRepository = new Lazy<ILetterRepository>(() 
            => new LetterRepository(repositoryContext));
        }

        public IDocumentCategoryRepository DocumentCategory =>
            _documentCategoryRepository.Value;
        public IDocumentStatusRepository DocumentStatus => 
            _documentStatusRepository.Value;
        public IDocumentRepository Document => _documentRepository.Value;
        public ILetterRepository Letter => _letterRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
    }

}
