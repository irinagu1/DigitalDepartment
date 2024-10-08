using Contracts.Auth;
using Entities.Models.Auth;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Auth
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        private readonly RepositoryContext _context;
        public UserRoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }

        public void DeleteRolesForUser(string userId)
        {
            var rows = _context.UserRoles.Where(ur => ur.UserId == userId);
            _context.UserRoles.RemoveRange(rows);
            _context.SaveChanges();
        }

        public int GetAmountOfUsersByRoleId(string roleId)
        {
            var count = FindByCondition(ur => ur.RoleId == roleId, false).Count();
            return count;
        }
    }
}
