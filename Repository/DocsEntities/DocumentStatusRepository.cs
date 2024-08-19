using Contracts.DocsEntities;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentStatusRepository : RepositoryBase<DocumentStatusRepository>, IDocumentStatusRepository
    {
        public DocumentStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
