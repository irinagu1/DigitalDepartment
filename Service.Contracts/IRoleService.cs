using Shared.DataTransferObjects.Roles;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IRoleService
    {
        Task<IEnumerable<RolesDto>> GetByUserId(string userId);
        Task<List<RolesForLettersDto>> GetAllRolesForLetters(RolesParameters parameters);
        Task<List<RolesForLettersDto>> GetAllRolesForLetters();
        Task<List<RolesDto>> GetRolesWithParameters(RolesParameters parameters);
        RolesDto GetRoleById(string roleId);
        RolesDto CreateRole(InfoForCreationDto infoForCreationDto);
        RolesDto UpdateRole(string roleId, InfoForCreationDto infoForCreationDto);
        bool UpdateRole(string roleId);
        Task DeleteRoleAsync(string roleId, bool trackChanges);
    }
}
