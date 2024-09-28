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
    public class PermissionRoleRepository : RepositoryBase<PermissionRole>, IPermissionRoleRepository
    {
        private readonly RepositoryContext _context;
        public PermissionRoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public Task<IEnumerable<PermissionRole>> GetPermissionRoleByRoleId()
        {
            throw new NotImplementedException();
        }

        public void CreatePermissionRole(PermissionRole permissionRole) => Create(permissionRole);

        public void DeleteAllePermissionRole(string roleId)
        {
            var rows = _context.PermissionRoles.Where(pr => pr.RoleId == roleId);
            _context.PermissionRoles.RemoveRange(rows);
            _context.SaveChanges();
        }
    }
}
