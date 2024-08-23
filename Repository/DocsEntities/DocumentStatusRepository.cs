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

        //все
        //фильтр сюда же ( активно-не активно)
        public async Task<IEnumerable<DocumentStatus>> GetAllDocumentStatusesAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(dc => dc.Name)
                .ToListAsync();

        //один конкретный
        public async Task<DocumentStatus> GetDocumentStatusAsync(int documentStatusId, bool trackChanges)
            => await FindByCondition(dc => dc.Id.Equals(documentStatusId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateDocumentStatus(DocumentStatus documentStatus) => Create(documentStatus);

        public void DeleteDocumentStatus(DocumentStatus documentStatus) => Delete(documentStatus);
    }
}
