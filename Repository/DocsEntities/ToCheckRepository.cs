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
    public class ToCheckRepository : RepositoryBase<ToCheck>, IToCheckRepository
    {
        public ToCheckRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
