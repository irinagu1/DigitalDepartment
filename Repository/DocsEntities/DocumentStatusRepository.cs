using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class DocumentStatusRepository 
        : RepositoryBase<DocumentStatus>, IDocumentStatusRepository
    {
        public DocumentStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<DocumentStatus>> GetAllDocumentStatuses(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(dc => dc.Name)
                .ToListAsync();

        public async Task<DocumentStatus> GetDocumentStatus(int documentStatusId, bool trackChanges)
            => await FindByCondition(dc => dc.Id.Equals(documentStatusId), trackChanges)
            .SingleOrDefaultAsync();
  
    }
}
