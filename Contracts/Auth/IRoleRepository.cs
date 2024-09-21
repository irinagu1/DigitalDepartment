using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRoles();
    }
}
