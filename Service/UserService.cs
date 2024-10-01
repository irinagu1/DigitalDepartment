using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions.NotFound;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
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

        public UserDto GetUserById(string userId)
        {

            var user = _repository.User.GetUserById(userId);
            if (user is null)
                throw new UserNotFound(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<UserDto> GetUserByIdAsync(string userId)
        {
            var user =await _repository.User.GetUserByIdAsync(userId);
            if (user is null)
                throw new UserNotFound(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<List<UserForLettersDto>> GetAllUserForLetters()
        {
            var users =  await _repository.User.GetAllUsersByActive(true);
            var usersForLettersDto = _mapper.Map<List<UserForLettersDto>>(users);
            return usersForLettersDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersForShow(string activity)
        {
            bool isActive = bool.Parse(activity);
            var users = await _repository.User.GetAllUsersByActive(isActive);
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public async Task<HashSet<string>> GetUserPermissions(string userId)
        {
            var permissions = await _repository.User.GetUserPermissions(userId);
            return permissions;
        }

      
    }
}
