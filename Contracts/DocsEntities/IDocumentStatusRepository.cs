using Entities.Models;
using Shared.RequestFeatures;
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
        Task<PagedList<DocumentStatus>> GetAllDocumentStatusesAsync(
            DocumentStatusParameters documentStatusParameters, bool trackChanges);
        //get one by id
        Task<DocumentStatus> GetDocumentStatusAsync(int documentStatusId, bool trackChanges);

        //create new
        void CreateDocumentStatus(DocumentStatus documentStatus);

        //delete
        void DeleteDocumentStatus(DocumentStatus documentStatus);
        
        //update

    }
}
