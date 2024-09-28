using Entities.Models.Auth;
using Shared.RequestFeatures;
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
        Task<List<Role>> GetRolesWithParams(RolesParameters rolesParameters, bool trackChanges);
        Task<Role> GetRoleAsync(string id, bool trackChanges);
        Task<Role> GetRoleByNameAsync(string name, bool trackChanges);
        Role GetRoleByName(string name, bool trackChanges);
        Role GetRole(string id, bool trackChanges);
        void Create(Role role);
        void Update(Role role);
    }
}
