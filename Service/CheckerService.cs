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

        public async Task<DocumentStatus> GetDocumentEntityAndCheckifitExistsAsync(int id, bool trackChanges)
        {
            var documentStatus = await _repository.DocumentStatus.GetDocumentStatusAsync(id, trackChanges);
            if (documentStatus is null)
                throw new DocumentStatusNotFoundException(id);
            return documentStatus;
        }

    }
}
