using AutoMapper;
using Contracts.RepositoryCore;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Service.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDocumentStatusService> _documentStatusService;
        private readonly Lazy<IDocumentCategoryService> _documentCategoryService;
        private readonly Lazy<IDocumentService> _documentService;
        private readonly Lazy<ILetterService> _letterService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, ICheckerService checker)
        {
            _documentStatusService = new Lazy<IDocumentStatusService>
                (() => new DocumentStatusService(repositoryManager, mapper, checker));
            _documentCategoryService = new Lazy<IDocumentCategoryService>
                (() => new DocumentCategoryService(repositoryManager, mapper, checker));
            _documentService = new Lazy<IDocumentService>
                (() => new DocumentService(repositoryManager, mapper));
            _letterService = new Lazy<ILetterService>
                (() => new LetterService(repositoryManager, mapper));
        }

        public IDocumentStatusService DocumentStatusService 
            => _documentStatusService.Value;
        public IDocumentCategoryService DocumentCategoryService
            => _documentCategoryService.Value;
        public IDocumentService DocumentService 
            => _documentService.Value;
        public ILetterService LetterService 
            => _letterService.Value;
    }
}
