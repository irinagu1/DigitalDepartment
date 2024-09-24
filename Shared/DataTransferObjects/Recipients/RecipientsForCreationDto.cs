using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Recipients
{
    public record RecipientsForCreationDto
    {
        public int LetterId { get; init; }
        public string[]? RolesIds { get; init; }
        public string[]? UsersIds { get; init; }
        public bool ToCheck { get; init; }
    }
}
