using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotFound
{
    public sealed class LetterNotFoundException : NotFoundException
    {
        public LetterNotFoundException(int id)
         : base($"The letter with id: {id} doesnt exist in the database")
        {

        }
    }
}
