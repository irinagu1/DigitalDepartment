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
    public class ToCheckRepository : RepositoryBase<ToCheck>, IToCheckRepository
    {
        public ToCheckRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateToCheck(ToCheck toCheck) => Create(toCheck);

        public async Task<ToCheck> GetToCheckByUserAndDocumentIds(string userId, int documentId)
            => await FindByCondition(tc => tc.UserId == userId /*&& tc.DocumentId == documentId*/, false).
                FirstOrDefaultAsync();

      
    }
}
