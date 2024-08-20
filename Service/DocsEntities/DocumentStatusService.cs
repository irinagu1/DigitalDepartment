﻿using Contracts.RepositoryCore;
using Entities.Models;

using Service.Contracts.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class DocumentStatusService : IDocumentStatusService
    {
        private readonly IRepositoryManager _repository;
        public DocumentStatusService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public IEnumerable<DocumentStatus> 
            GetAllDocumentStatuses(bool trackChanges)
        {
            return _repository.DocumentStatus.GetAllDocumentStatuses(trackChanges);
        }
    }
}
