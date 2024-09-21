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
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly RepositoryContext _context;
        public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles;
        }
    }
}
