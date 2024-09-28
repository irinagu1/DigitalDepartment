using Entities.Models;
using Shared.DataTransferObjects.ToCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.DocsEntities
{
    public interface IToCheckService
    {
        Task<ToCheckDto> Create(string userId, int documentId);
    }
}
