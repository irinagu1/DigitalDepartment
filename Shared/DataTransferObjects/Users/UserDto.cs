using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Users
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public bool? isActive { get; set; }
        public bool? CanDelete { get; set; }
        public int? PositionId { get; set; }
        public string? PositionName { get; set; }
    }
}
