using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IToCheckRepository
    {
        Task<ToCheck?> GetToCheckByUserAndVersionId
            (string userId, long versionId);
     
        void CreateToCheck(ToCheck toCheck);


    }
}
