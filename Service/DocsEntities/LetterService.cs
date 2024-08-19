using Contracts.RepositoryCore;
using Service.Contracts.DocsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class LetterService : ILetterService
    {
        private readonly IRepositoryManager _repository;
        public LetterService(IRepositoryManager repository)
        {
            _repository = repository;
        }
    }
}
