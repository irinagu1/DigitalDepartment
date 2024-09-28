using AutoMapper;
using Contracts.RepositoryCore;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public PermissionService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PermissionDto>> GetAllPermissions()
        {
            var permissonsEntities = await _repository.Permission.GetAllPermissions(); 
            var permissionsDto = _mapper.Map<List<PermissionDto>>(permissonsEntities);
            return permissionsDto;
        }

        public List<string> GetPermissionCategories()
        {
            var categories = _repository.Permission.GetPermissionCategories();
            return categories;
        }

        public async Task<IEnumerable<PermissionDto>> GetPermissionsForRoleById(string roleId)
        {
            var permsEntities = await _repository.Permission.GetAllPermissionsForRoleAsync(roleId, false);
            var permsDto = _mapper.Map<IEnumerable<PermissionDto>>(permsEntities);
            return permsDto;
        }
    }
}
