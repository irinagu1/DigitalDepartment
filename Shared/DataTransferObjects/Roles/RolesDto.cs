using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Roles
{
    public class RolesDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? isActived { get; set; }
        public int? ConnectedUsers { get; set; }
    }
}
