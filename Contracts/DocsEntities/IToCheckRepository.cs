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
        Task<ToCheck> GetToCheckByUserAndDocumentIds(string userId, int documentId);
        void CreateToCheck(ToCheck toCheck);
    }
}
