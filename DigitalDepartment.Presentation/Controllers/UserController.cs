using Entities.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
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
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<HashSet<string>> GetUserPermissions()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var permissions = await _service.UserService.GetUserPermissions(userId);
            return permissions;
        }
    }
}
