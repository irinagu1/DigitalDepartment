using Contracts.RepositoryCore;
using Service.Contracts.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class DocumentService : IDocumentService
    {
        private readonly IRepositoryManager _repository;
        public DocumentService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}
