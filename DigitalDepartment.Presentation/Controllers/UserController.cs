using Entities.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Users;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersForLetters()
        {
            var users = await _service.UserService.GetAllUserForLetters();
            return Ok(users);
        }


        [HttpGet("byid")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            var users = await _service.UserService.GetUserByIdAsync(id);
            return Ok(users);
        }


        [HttpGet("forshow")]
        public async Task<IActionResult> GetAllUsersForShow([FromQuery]string isActive)
        {
            var users = await _service.UserService.GetAllUsersForShow(isActive);
            return Ok(users);
        }


        [HttpGet("permissions")]
        public async Task<HashSet<string>> GetUserPermissions()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var permissions = await _service.UserService.GetUserPermissions(userId);
            return permissions;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromQuery] string id, 
            [FromBody] UserForUpdateDto userForUpdateDto)
        {
            //   var updated =
            return NoContent();
        }
    }
}
