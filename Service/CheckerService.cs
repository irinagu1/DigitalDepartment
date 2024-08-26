using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class CheckerService : ICheckerService
    {
        private readonly IRepositoryManager _repository;
        public CheckerService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<DocumentCategory> GetDocumentCategoryEntityAndCheckiIfItExistsAsync(int id, bool trackChanges)
        {
            var documentCategory = await _repository.DocumentCategory.GetDocumentCategoryAsync(id, trackChanges);
            if(documentCategory is null)
                throw new DocumentCategoryNotFoundException(id);
            return documentCategory;
        }

        public async Task<DocumentStatus> GetDocumentStatusEntityAndCheckIfItExistsAsync(int id, bool trackChanges)
        {
            var documentStatus = await _repository.DocumentStatus.GetDocumentStatusAsync(id, trackChanges);
            if (documentStatus is null)
                throw new DocumentStatusNotFoundException(id);
            return documentStatus;
        }

    }
}
