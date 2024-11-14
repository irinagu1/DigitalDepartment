using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions.NotFound;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Roles;
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
        private readonly ICheckerService _checker;
        public UserService(IRepositoryManager repository, IMapper mapper, ICheckerService checker)
        {
            _repository = repository;
            _mapper = mapper;
            _checker = checker;
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

        public bool UpdateUserStatus(string userId)
        {
            var excistedUser = _checker.GetUserEntityAndCheckItExists(userId, false);
            excistedUser.isActive = !excistedUser.isActive;
            _repository.User.Update(excistedUser);
            _repository.Save();
            return true;
        }

        public bool UpdateUser(UserForUpdateDto userForUpdateDto)
        {
            var excistedUser = _checker.GetUserEntityAndCheckItExists(userForUpdateDto.Id, false);
            if (UpdateUserEntity(userForUpdateDto, excistedUser) && UpdateUserRoles(userForUpdateDto.RolesNames, userForUpdateDto.Id))
                return true;
            else throw new Exception("cannot update");
        }

        public bool UpdateUserEntity(UserForUpdateDto updateDto, User userEntity)
        {
            userEntity.FirstName = updateDto.FirstName; 
            userEntity.SecondName = updateDto.SecondName;
            userEntity.LastName = updateDto.LastName;
            userEntity.UserName = updateDto.UserName;
            userEntity.Email = updateDto.UserName + "@mail.ru";
            _repository.User.Update(userEntity);
            _repository.Save();
            return true;
        }

        public bool UpdateUserRoles(IEnumerable<string>? Roles, string userId)
        {
            _repository.UserRole.DeleteRolesForUser(userId);
            foreach(var roleName in Roles)
            {
                var role = _repository.Role.GetRoleByName(roleName, false);
                UserRoleDto userRoleDto = new UserRoleDto()
                {
                    UserId = userId,
                    RoleId = role.Id,
                };
                var entity = _mapper.Map<UserRole>(userRoleDto);
                _repository.UserRole.Create(entity);
            }

            _repository.Save();
            return true;
        }

        public bool ChangePassword(PasswordToChangeDto changeDto)
        {
            return true;
          //  var userEntity = _checker.GetUserEntityAndCheckItExists(changeDto.UserId, false);
        //    userEntity.PasswordHash
        }

        public async Task<IEnumerable<UserDto>> GetUsersByRole(string roleId)
        {
            var usersEntities = await _repository.User.GetUsersByRoleId(roleId);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersEntities);
            return usersDto;
        }

        public async Task<UserAndRolesDto> GetInfoAboutUser(string userId)
        {
            var user = GetUserById(userId);
            
            var rolesEntities = await _repository.Role.GetByUserId(userId);
            var rolesDto = _mapper.Map<IEnumerable<RolesDto>>(rolesEntities);

            return new UserAndRolesDto() { User = user, Roles = rolesDto };
        }
    }
}
