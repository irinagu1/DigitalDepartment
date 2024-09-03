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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly RepositoryContext _context;
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public async Task<HashSet<string>> GetUserPermissions(string userId)
        {
            var permissions = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        join pr in _context.PermissionRoles on r.Id equals pr.RoleId
                        join p in _context.Permissions on pr.PermissionId equals p.Id
                        where u.Id == userId
                        select new
                        {
                            p.Name
                        };
            HashSet<string> uniquePermissions = new HashSet<string>((IEnumerable<string>)permissions);
            return uniquePermissions;
        }
    }
}
