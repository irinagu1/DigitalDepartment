using Contracts.DocsEntities;
using Entities.Models;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentVersionRepository :
        RepositoryBase<DocumentVersion>, IDocumentVersionRepository
    {
        private RepositoryContext _repositoryContext;
      
        public DocumentVersionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
    }
}
