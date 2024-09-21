using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models.Auth;
using Service.Contracts;
using Shared.DataTransferObjects.Users;
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
        private readonly IMapper _mapper;
        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<UserForLettersDto>> GetAllUserForLetters()
        {
            var users =  await _repository.User.GetAllUsers();
            var usersForLettersDto = _mapper.Map<List<UserForLettersDto>>(users);
            return usersForLettersDto;
        }

        public async Task<HashSet<string>> GetUserPermissions(string userId)
        {
            var permissions = await _repository.User.GetUserPermissions(userId);
            return permissions;
        }
    }
}
