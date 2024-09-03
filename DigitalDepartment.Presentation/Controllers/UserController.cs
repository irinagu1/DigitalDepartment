using Entities.Models.Auth;
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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<HashSet<string>> GetUserPermissions(string userId)
        {
            var permissions = await _service.UserService.GetUserPermissions(userId);
            return permissions;
        }
    }
}
