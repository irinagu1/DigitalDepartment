using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class DocumentVersionNotFoundException : NotFoundException
    {
        public DocumentVersionNotFoundException(long id)
            : base($"The document category with id: {id} doesnt exist in the database")
        {

        }
    }
}
