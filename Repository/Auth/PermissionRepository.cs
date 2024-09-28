using Contracts.Auth;
using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Auth
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        private readonly RepositoryContext _context;
        public PermissionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<Permission>> GetAllPermissions()
        {
            var permissions = await FindAll(false).ToListAsync();
            return permissions;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsForRoleAsync(string roleId, bool trackChanges)
        {
            var permissions = await _context.PermissionRoles
                  .Where(pr => pr.RoleId == roleId)
                  .Select(pr => pr.Permission)
                  .ToListAsync();
            return permissions;
        }

        public Permission GetPermissionByName(string name, bool trackChanges)
        {
            var permission = FindByCondition(p => p.Name == name, false).FirstOrDefault();
            return permission;
        }

        public async Task<Permission> GetPermissionByNameAsync(string name, bool trackChanges)
        {
            var permission = await FindByCondition(p => p.Name == name, false).FirstOrDefaultAsync();
            return permission;
        }

        public List<string> GetPermissionCategories()
        {
            var distinctCategories = from p in _context.Permissions
                                     group new { p.Category } by p.Category into uniqCats
                                     select uniqCats.FirstOrDefault();
            List<string> list = new List<string>();
            foreach (var cat in distinctCategories) {
                list.Add(cat.Category);
            }
            return list;
        }
    }
}
