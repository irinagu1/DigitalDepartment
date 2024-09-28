using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetAllPermissions();
        Task<Permission> GetPermissionByNameAsync(string name, bool trackChanges);
        Permission GetPermissionByName(string name, bool trackChanges);
        List<string> GetPermissionCategories();
        Task<IEnumerable<Permission>> GetAllPermissionsForRoleAsync(string roleId, bool trackChanges);
    }
}
