using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserRoleDto
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
