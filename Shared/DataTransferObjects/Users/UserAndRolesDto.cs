using Shared.DataTransferObjects.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Users
{
    public class UserAndRolesDto
    {
        public UserDto User { get; set; }
        public IEnumerable<RolesDto>? Roles { get; set; }    
    }
}
