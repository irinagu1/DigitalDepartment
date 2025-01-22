using Contracts.Auth;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Auth
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly RepositoryContext _context;
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<Role>> GetAllRoles(RolesParameters rolesParameters)
        {
            if (rolesParameters.isActive is not null)
            {
                var roles = await _context.Roles.Where(r => r.IsActived == rolesParameters.isActive).ToListAsync();
                return roles;
            }
            else
            {
                var roles = await _context.Roles.ToListAsync();
                return roles;
            }
        }

        public async Task<List<Role>> GetAllRoles()
        {
          
                var roles = await _context.Roles.ToListAsync();
                return roles;
            
        }
        public Role GetRole(string id, bool trackChanges)
        {
            var role = FindByCondition(r => r.Id == id, trackChanges).SingleOrDefault();
            if (role is null)
                throw new RoleNotFoundException(id);
            return role;
        }

        public async Task<Role> GetRoleAsync(string id, bool trackChanges)
        {
            var role = await FindByCondition(r => r.Id == id, trackChanges)
                .SingleOrDefaultAsync();
            if (role is null)
                throw new RoleNotFoundException(id);
            return role;
        }

        public async Task<List<Role>> GetRolesWithParams(RolesParameters rolesParameters, bool trackChanges)
        {
            var roles = await FindByCondition(r => r.IsActived == rolesParameters.isActive, trackChanges)
                        .ToListAsync();
            return roles;
        }

        public void CreateRole(Role role) => Create(role);
        public void UpdateRole(Role role) => Update(role);
        public void DeleteRole(Role role) => Delete(role);
        public async Task<Role> GetRoleByNameAsync(string name, bool trackChanges)
        {
            var role = await FindByCondition(r => r.Name == name, trackChanges).FirstOrDefaultAsync();
            return role;
        }
        public Role GetRoleByName(string name, bool trackChanges)
        {
            var role = FindByCondition(r => r.Name == name, trackChanges).FirstOrDefault();
            return role;
        }

        public async Task<IEnumerable<Role>> GetByUserId(string userId)
        {
            var roles = await _context.UserRoles
                 .Where(ur => ur.UserId == userId)
                 .Select(r => r.Role)
                 .ToListAsync();
            return roles;
        }
    }
}
