using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class RolesParameters
    {
        public bool? isActive { get; set; } = true;
        public bool? WithUsersAmount { get; set; } = false;
    }
}
