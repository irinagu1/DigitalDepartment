using Contracts.DocsEntities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Extensions;
using Shared.RequestFeatures;
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
        public async Task<PagedList<DocumentStatus>> GetAllDocumentStatusesAsync(
             DocumentStatusParameters documentStatusParameters, bool trackChanges)
        {
            var documentStatuses = await FindAll(trackChanges)
                                      .FilterDocumentStatuses(documentStatusParameters)
                                     .OrderByDescending(dc => dc.isEnable)
                                     .Skip((documentStatusParameters.PageNumber-1)* documentStatusParameters.PageSize)
                                     .Take(documentStatusParameters.PageSize)
                                     .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<DocumentStatus>(documentStatuses,
                                                count,
                                                documentStatusParameters.PageNumber,
                                                documentStatusParameters.PageSize);
        }

        //один конкретный
        public async Task<DocumentStatus> GetDocumentStatusAsync(int documentStatusId, bool trackChanges)
            => await FindByCondition(dc => dc.Id.Equals(documentStatusId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateDocumentStatus(DocumentStatus documentStatus) => Create(documentStatus);

        public void DeleteDocumentStatus(DocumentStatus documentStatus) => Delete(documentStatus);
    }
}
