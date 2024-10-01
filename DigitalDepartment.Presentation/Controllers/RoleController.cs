using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Roles;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _service;
        public RoleController(IServiceManager service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _service.RoleService.GetAllRolesForLetters();
            return Ok(roles);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUsersRoles([FromQuery]string  id)
        {
            var roles = await _service.RoleService.GetByUserId(id);
            return Ok(roles);
        }

        [HttpGet("GetWithParameters")]
        public async Task<IActionResult> GetRolesWithParameters([FromQuery] RolesParameters rolesParameters)
        {
            var roles = await _service.RoleService.GetRolesWithParameters(rolesParameters);
            return Ok(roles);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetRoleById([FromQuery] string roleId)
        {
            var roleDto = _service.RoleService.GetRoleById(roleId);
            return Ok(roleDto);
        }

      
        [HttpPost("create")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateRole([FromBody] InfoForCreationDto infoForCreationDto)
        {
            var roles = _service.RoleService.CreateRole(infoForCreationDto);
            return Ok(roles);
        }

        [HttpPost("update")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateRole([FromQuery] string roleId,
            [FromBody] InfoForCreationDto infoForCreationDto)
        {
           var role = _service.RoleService.UpdateRole(roleId, infoForCreationDto);
            return Ok();
        }

        [HttpPost("toArchive")]
        public async Task<IActionResult> ToArchiveRole([FromQuery] string roleId)
        {
            var role = _service.RoleService.UpdateRole(roleId);
            return Ok(role);
        }

    }
}
