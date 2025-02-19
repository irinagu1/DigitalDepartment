﻿using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Auth
{
    public interface IUserRepository
    {
        int AmountOfConnectedUsersByPositionId(int positionId);
        User GetUserById(string userId);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserByIdAsync(string userId);
        Task<HashSet<string>> GetUserPermissions(string userId);
        Task<IEnumerable<User>> GetAllUsersByActive(bool param);
        Task<HashSet<string>> GetUserRolesIds(string userId);
        Task<IEnumerable<User>> GetUsersByRoleId(string roleId);
        Task<IEnumerable<User>> GetUsersForDeleting();
        User GetUserByLetterIdAsync(int letterId);
        void Update(User user);
        void UpdateUser(User user);
    }
}
