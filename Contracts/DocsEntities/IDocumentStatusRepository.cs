using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IDocumentStatusRepository
    {
        IEnumerable<DocumentStatus> GetAllDocumentStatuses(bool trackChanges);
        DocumentStatus GetDocumentStatus(int documentStatusId, bool trackChanges);
    }
}
