using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IDocumentStatusService
    {
        IEnumerable<DocumentStatus> GetAllDocumentStatuses(bool trackChanges);
    }
}
