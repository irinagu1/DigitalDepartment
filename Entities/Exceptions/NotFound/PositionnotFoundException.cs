using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class PositionnotFoundException : NotFoundException
    {
        public PositionnotFoundException(int id)
            : base($"Position with id: {id} doesnt exist in the database")
        {

        }
    }
}
