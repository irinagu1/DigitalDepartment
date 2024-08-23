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
        //get all with filtering
        Task<IEnumerable<DocumentStatus>> GetAllDocumentStatusesAsync(bool trackChanges);
        //get one by id
        Task<DocumentStatus> GetDocumentStatusAsync(int documentStatusId, bool trackChanges);

        //create new
        void CreateDocumentStatus(DocumentStatus documentStatus);

        //delete
        void DeleteDocumentStatus(DocumentStatus documentStatus);
        
        //update

    }
}
