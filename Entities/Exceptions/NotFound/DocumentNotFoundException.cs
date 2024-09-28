using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class DocumentNotFoundException : NotFoundException
    {
        public DocumentNotFoundException(int id)
            : base($"The document  with id: {id} doesnt exist in the database")
        {

        }
    }
}
