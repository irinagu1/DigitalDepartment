using Contracts.DocsEntities;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentRepository : RepositoryBase<DocumentRepository>, IDocumentRepository
    {
        public DocumentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
