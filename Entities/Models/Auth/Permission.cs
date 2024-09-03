using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.Auth
{
    public class Permission
    {
        [Column("PermissionId")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<PermissionRole>? PermissionRoles { get; set; }
    }
}
