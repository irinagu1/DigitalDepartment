using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class VersionParameters : RequestParameters
    {
        //filter
        public bool ToCheck { get; set; }

        public string? Category { get; set; }
        public string? Status { get; set; }
        public string CreationDate { get; set; }

        //search
        public string? SearchByName { get; set; }
        public string? SearchByAuthor { get; set; }
    }
}
