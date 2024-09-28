using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class RoleNotFoundException : NotFoundException
    {
        public RoleNotFoundException(string id)
          : base($"The role with id: {id} doesnt exist in the database")
        {

        }
    }
}
