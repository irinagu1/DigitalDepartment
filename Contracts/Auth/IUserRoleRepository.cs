using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IUserRoleRepository
    {
        int GetAmountOfUsersByRoleId(string roleId);
        void DeleteRolesForUser(string userId);
        void Create(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
