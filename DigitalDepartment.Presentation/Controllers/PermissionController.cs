using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public PermissionController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _service.PermissionService.GetAllPermissions();
            return Ok(permissions);
        }

        [HttpGet("GetByRoleId")]
        public async Task<IActionResult> GetPermissionsForRoleById([FromQuery] string roleId)
        {
            var permissionsDto = await _service.PermissionService.GetPermissionsForRoleById(roleId);
            return Ok(permissionsDto);
        }

        [HttpGet("categories")]
        public IActionResult GetAllPermissionCategories()
        {
            var categories = _service.PermissionService.GetPermissionCategories();
            return Ok(categories);
        }

    }
}
