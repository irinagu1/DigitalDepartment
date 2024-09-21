using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Users
{
    public record UserForLettersDto
    {
        public string? UserId { get; init; }
        public string? UserName { get; init; }
    }
}
