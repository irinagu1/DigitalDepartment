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
    public class DocumentStatusRepository 
        : RepositoryBase<DocumentStatus>, IDocumentStatusRepository
    {
        public DocumentStatusRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<DocumentStatus> GetAllDocumentStatuses(bool trackChanges)
            => FindAll(trackChanges)
                .OrderBy(dc => dc.Name)
                .ToList();

        public DocumentStatus GetDocumentStatus(int documentStatusId, bool trackChanges) =>
            FindByCondition(dc => dc.Id.Equals(documentStatusId), trackChanges)
            .SingleOrDefault();
  
    }
}
