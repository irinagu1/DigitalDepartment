using Contracts.Auth;
using Entities.Exceptions.NotFound;
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

        public async Task<IEnumerable<User>> GetAllUsersByActive(bool param)
        {
            var users = await _context.Users.Where(u => u.isActive == param).ToListAsync();
            return users;
        }

        public async Task<HashSet<string>> GetUserRolesIds(string userId)
        {
            var rolesIds = from r in _context.Roles
                           join ur in _context.UserRoles on r.Id equals ur.RoleId
                           join u in _context.Users on ur.UserId equals u.Id
                           where u.Id == userId
                           select new
                           {
                               r.Id
                           };
            List<string> rolesToList = new List<string>();
            foreach (var item in rolesIds) { rolesToList.Add(item.Id); }
            //List<string> list = permissions.ToList<string>();
            HashSet<string> uniqueRoles = new HashSet<string>(rolesToList);
            return uniqueRoles;
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
            Console.WriteLine(permissions.Count());
            foreach(var item in permissions) { Console.WriteLine(item.Name); }
            List<string> permissionsToList = new List<string>();
            foreach (var item in permissions) { permissionsToList.Add(item.Name); }
            //List<string> list = permissions.ToList<string>();
            HashSet<string> uniquePermissions = new HashSet<string>(permissionsToList);
            return uniquePermissions;
        }

        public User GetUserById(string userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
           
            return user;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
           
            return user;
        }
        public User GetUserByLetterIdAsync(int letterId)
        {
            var user = _context.Letters.Include(l => l.Author)
                .Where(l => l.Id == letterId)
                .Select(l => l.Author).FirstOrDefault();
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public void UpdateUser(User user) => Update(user);

        public async Task<IEnumerable<User>> GetUsersByRoleId(string roleId)
        {
            var users = await _context.Users.Where(u => u.UserRoles.Any(ur => ur.RoleId == roleId)).ToListAsync();
            return users;
        }

        public int AmountOfConnectedUsersByPositionId(int positionId)
        {
            var count = FindByCondition
                (u => u.PositionId == positionId, false).Count();
            return count;

        }

        public async Task<IEnumerable<User>> GetUsersForDeleting()
        {
            var allUsers = await _context.Users.ToListAsync();
            var usersToDelete = new List<User>();
            for(int i=0; i < allUsers.Count; i++)
            {
                var inRecipients = _context.Recipients.Where(
                    r => r.Type == "user" &&
                    r.TypeId == allUsers[i].Id).ToList();
                if (inRecipients.Count != 0)
                    continue;

                var inToChecks = _context.ToChecks.Where(
                    ch => ch.UserId == allUsers[i].Id).ToList();
                if (inToChecks.Count != 0)
                    continue;
                usersToDelete.Add(allUsers[i]);
            }
            return usersToDelete;
        }


    }
}
