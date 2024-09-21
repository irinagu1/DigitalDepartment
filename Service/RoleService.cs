using AutoMapper;
using Contracts.RepositoryCore;
using Service.Contracts;
using Shared.DataTransferObjects.Roles;
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
        public RoleService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<RolesForLettersDto>> GetAllRolesForLetters()
        {
            var roles = await _repository.Role.GetAllRoles();
            var rolesForLettersDto = _mapper.Map<List<RolesForLettersDto>>(roles);
            return rolesForLettersDto;
        }
    }
}
