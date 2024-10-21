using Shared.DataTransferObjects.Roles;
using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Recipients
{
    public class AllRecipientsByCategoryDto
    {
        public IEnumerable<UserDto> UserToShow { get; set; } = [];
        public IEnumerable<UserDto> UserToCheck { get; set; } = [];

        public IEnumerable<RolesDto> RolesToShow { get; set; } = [];
        public IEnumerable<RolesDto> RolesToCheck { get; set; } = [];
    }
}
