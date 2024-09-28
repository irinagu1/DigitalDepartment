using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IPermissionRoleRepository
    {
        Task<IEnumerable<PermissionRole>> GetPermissionRoleByRoleId();
        void CreatePermissionRole(PermissionRole permissionRole);
        void DeleteAllePermissionRole(string roleId);
    }
}
