using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RoleEntity : IdentityRole
    {
        public ICollection<PermissionRoleEntity>? PermissionRoleEntities { get; set; }
        public ICollection<User>? Users { get; set; } 
    }
}
