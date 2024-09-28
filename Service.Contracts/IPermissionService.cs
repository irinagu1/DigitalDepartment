using Entities.Models.Auth;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPermissionService
    {
        Task<List<PermissionDto>> GetAllPermissions();
        Task<IEnumerable<PermissionDto>> GetPermissionsForRoleById(string roleId);
        List<string> GetPermissionCategories();
    }
}
