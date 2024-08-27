using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class NotSingleException : Exception
    {
        protected NotSingleException(string message)
        : base(message)
        { }
    }
}
