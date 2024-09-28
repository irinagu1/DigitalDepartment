using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.NotSingle
{
    public sealed class DocumentNotSingleException : NotSingleException
    {
        public DocumentNotSingleException(string path)
            : base($"The document with path {path} already exists in the database")
        {

        }
    }
}
