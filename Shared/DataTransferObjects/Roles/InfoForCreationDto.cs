using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Roles
{
    public class InfoForCreationDto
    {
        [Required(ErrorMessage = "Role Name is a required field.")]
        public string? RoleName { get; set; }
        public IEnumerable<string>? Permissions { get; set;}
    }
}
