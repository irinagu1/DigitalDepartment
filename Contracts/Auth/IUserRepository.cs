using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IUserRepository
    {
        Task<HashSet<string>> GetUserPermissions(string userId);
        Task<List<User>> GetAllUsers();
        Task<HashSet<string>> GetUserRolesIds(string userId);
    }
}
