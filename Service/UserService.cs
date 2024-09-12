using Contracts.RepositoryCore;
using Entities.Models.Auth;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service


{
    
    public sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        public UserService(IRepositoryManager repository)
        {
            _repository = repository;
        }   

        public async Task<HashSet<string>> GetUserPermissions(string userId)
        {
            var permissions = await _repository.User.GetUserPermissions(userId);
            return permissions;
        }
    }
}
