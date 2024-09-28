using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotSingle
{
    public sealed class RoleNotSingleException : NotSingleException
    {
        public RoleNotSingleException(string name)
         : base($"The role with name {name} already exists in the database")
        {

        }
    }
}
