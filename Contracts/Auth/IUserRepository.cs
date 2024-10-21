using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IUserRepository
    {
        User GetUserById(string userId);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserByIdAsync(string userId);
        Task<HashSet<string>> GetUserPermissions(string userId);
        Task<IEnumerable<User>> GetAllUsersByActive(bool param);
        Task<HashSet<string>> GetUserRolesIds(string userId);
        Task<IEnumerable<User>> GetUsersByRoleId(string roleId);
        User GetUserByLetterIdAsync(int letterId);
        void Update(User user);
    }
}
