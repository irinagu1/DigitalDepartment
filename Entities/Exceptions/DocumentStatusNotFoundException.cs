using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DocumentStatusNotFoundException : NotFoundException
    {
        public DocumentStatusNotFoundException(int id)
            : base($"The documents status with id: {id} doesnt exist in the database")
        { }
    }
}
