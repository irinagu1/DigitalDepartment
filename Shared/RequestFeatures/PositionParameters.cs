using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
   public class PositionParameters : RequestParameters
   {
        public bool? isEnable { get; set; }
        public bool WithUsersAmount { get; set; } = false;
    }
}
