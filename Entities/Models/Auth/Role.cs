using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Auth
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole>? UserRoles { get; set; }

        public virtual ICollection<PermissionRole>? PermissionRoles { get; set; }
        public bool IsActived { get; set; }
      
    }
}
