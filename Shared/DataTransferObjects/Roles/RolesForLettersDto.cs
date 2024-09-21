using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Roles
{
    public record RolesForLettersDto
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
    }
}
