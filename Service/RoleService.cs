using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions.NotSingle;
using Entities.Models.Auth;
using Service.Contracts;
using Shared.DataTransferObjects.PermissionRole;
using Shared.DataTransferObjects.Roles;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class RoleService : IRoleService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checkerService;
        public RoleService(IRepositoryManager repository, IMapper mapper, ICheckerService checkerService)
        {
            _repository = repository;
            _mapper = mapper;
            _checkerService = checkerService;
        }

        public RolesDto CreateRole(InfoForCreationDto infoForCreationDto)
        {
            var excistedRole = _repository.Role.GetRoleByName(infoForCreationDto.RoleName, false);
            if (excistedRole is not null)
                throw new RoleNotSingleException(infoForCreationDto.RoleName);

            RoleForCreationDto newRole = new RoleForCreationDto() 
            { 
                Name = infoForCreationDto.RoleName,
                NormalizedName = infoForCreationDto.RoleName.ToUpper(),
                isActived = true
            };
            var roleEntity = _mapper.Map<Role>(newRole);
            _repository.Role.Create(roleEntity);
           

            var roleDto = _mapper.Map<RolesDto>(roleEntity);

            //create role = role id
            
            foreach(var permName in infoForCreationDto.Permissions)
            {
                var permission = _repository.Permission.GetPermissionByName(permName, false);
                PermissionRoleDto prDto= new PermissionRoleDto() { PermissionId = permission.Id, RoleId = roleEntity.Id };
                
                var permEntity = _mapper.Map<PermissionRole>(prDto);
                _repository.PermissionRole.CreatePermissionRole(permEntity);
            }
            _repository.Save();

            return roleDto;
            //loop get perms and insert permrole with id

        }

        public async Task<List<RolesForLettersDto>> GetAllRolesForLetters()
        {
            var roles = await _repository.Role.GetAllRoles();
            var rolesForLettersDto = _mapper.Map<List<RolesForLettersDto>>(roles);
            return rolesForLettersDto;
        }

        public  RolesDto GetRoleById(string roleId)
        {
            var role = _repository.Role.GetRole(roleId, false);
            var roleDto = _mapper.Map<RolesDto>(role);
            return roleDto;
        }

        public async Task<List<RolesDto>> GetRolesWithParameters(RolesParameters parameters)
        {
            var roles = await _repository.Role.GetRolesWithParams(parameters, false);
            var rolesDto = _mapper.Map<List<RolesDto>>(roles);
            if (parameters.WithUsersAmount)
                rolesDto = GetAmountofUsersForRoles(rolesDto);
            return rolesDto;
        }

        public List<RolesDto> GetAmountofUsersForRoles(List<RolesDto> list)
        {
            foreach(var el in list)
            {
                var count = _repository.UserRole.GetAmountOfUsersByRoleId(el.Id);
                el.ConnectedUsers = count;
            }
            return list;
        }

        public RolesDto UpdateRole(string roleId, InfoForCreationDto infoForCreationDto)
        {
            var excestedRole = _repository.Role.GetRole(roleId, false);
            RoleForUpdateDto roleDto = new RoleForUpdateDto() 
            { 
                Id = excestedRole.Id,
                Name = infoForCreationDto.RoleName,
                IsActived = excestedRole.IsActived,
                NormalizedName = infoForCreationDto.RoleName.ToUpper() 
            };
            _mapper.Map(roleDto, excestedRole);
            _repository.Role.Update(excestedRole);
            _repository.Save();
            var newPerms = UpdatePermissionsForRole(roleId, infoForCreationDto.Permissions);
            if (!newPerms)
                throw new Exception("Cant update permissions");
            var role = _mapper.Map<RolesDto>(excestedRole);
            return role;
        }

        public bool UpdatePermissionsForRole(string roleId, IEnumerable<string> permissions) 
       
        {
            try
            {
                _repository.PermissionRole.DeleteAllePermissionRole(roleId);

                foreach (var permName in permissions)
                {
                    var permission = _repository.Permission.GetPermissionByName(permName, false);
                    PermissionRoleDto prDto = new PermissionRoleDto()
                    {
                        PermissionId = permission.Id,
                        RoleId = roleId
                    };

                    var permEntity = _mapper.Map<PermissionRole>(prDto);
                    _repository.PermissionRole.CreatePermissionRole(permEntity);
                }
                _repository.Save();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool UpdateRole(string roleId)
        {
            var roleEntity = _repository.Role.GetRole(roleId, false);
            RoleForUpdateDto roleDto = new RoleForUpdateDto()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                IsActived = !roleEntity.IsActived,
                NormalizedName = roleEntity.NormalizedName
            };
            _mapper.Map(roleDto, roleEntity);
            _repository.Role.Update(roleEntity);
            _repository.Save();
//            var roleToReturn = _mapper.Map<RolesDto>(roleEntity);

            return true;
        }

        public async Task<IEnumerable<RolesDto>> GetByUserId(string userId)
        {
            var roles = await _repository.Role.GetByUserId(userId);
            var rolesDto = _mapper.Map<IEnumerable<RolesDto>>(roles);
            return rolesDto;
        }

        public async Task DeleteRoleAsync(string roleId, bool trackChanges)
        {
            var roleEntity = await _checkerService.GetRoleEntityAndCheckIdExistsAsync(roleId, trackChanges);
            _repository.Role.Delete(roleEntity);
            await _repository.SaveAsync();
        }
    }
}
