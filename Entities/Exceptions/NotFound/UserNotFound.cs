using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class UserNotFound : NotFoundException
    {
        public UserNotFound(string id)
            : base($"User with id: {id} doesnt exist in the database")
        {

        }
    }
}
