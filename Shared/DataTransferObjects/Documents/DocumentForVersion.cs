using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
     public class DocumentForVersion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DocumentStatusId { get; set; }
        public string DocumentStatusName { get; set; }
        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryName { get; set; }
        public int? LetterId { get; set; }

    }
}
