using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PermissionRoleEntity
    {
        public string RoleEntityId { get; set; }
        public RoleEntity? RoleEntity {  get; set; }

        public int PermissionId {  get; set; }
        public Permission? Permission { get; set; }
    }
}
