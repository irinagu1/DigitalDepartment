using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class DocumentShowParameters : RequestParameters
    {
        public bool isArchived { get; set; }
        public string CreationDate { get; set; }
        public string? SearchByName { get; set; }
        public string? SearchByAuthor { get; set; }
    }
}
