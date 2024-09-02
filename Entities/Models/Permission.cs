using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Permission
    {
        [Column("PermissionId")]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<PermissionRoleEntity>? PermissionRoleEntities { get; set; }
    }
}
