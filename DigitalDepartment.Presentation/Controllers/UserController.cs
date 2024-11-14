using Entities.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Users;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet("userInfo")]
        public async Task<IActionResult> GetInfoAboutUser()
        {
            var userId = 
                HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId")?.
                Value.ToString();
            if (userId is null)
                return BadRequest();
            
            var info = await _service.UserService.GetInfoAboutUser(userId);
            return Ok(info);
        }

        [HttpGet("permissions")]
        public async Task<HashSet<string>> GetUserPermissions()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var permissions = await _service.UserService.GetUserPermissions(userId);
            return permissions;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(
            [FromBody] UserForUpdateDto userForUpdateDto)
        {
            var result = _service.UserService.UpdateUser(userForUpdateDto);
            if (result)
                return NoContent();
            return BadRequest(result);
        }

        [HttpPost("toArchive")]
        public async Task<IActionResult> ToArchiveUser([FromQuery] string userId)
        {
            var ifOk = _service.UserService.UpdateUserStatus(userId);
            return Ok(ifOk);
        }
     
        [HttpPut("password")]
        public async Task<IActionResult> ChangePassword(
         [FromBody] PasswordToChangeDto changeDto)
        {
            //var result = _service.UserService.UpdateUser(userForUpdateDto);
         //   if (result)
                return NoContent();

        }
    }
}
