using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.DocumentVersion
{
    public record DocumentVersionForCreationDto
    {
        public long? Number { get; set; }

        public bool? isLast { get; set; }

        public int DocumentId { get; set; }
   
        public string? Path { get; set; }

        public DateTime? CreationDate { get; set; }

        public string Message { get; set; } = "";
        public string? AuthorId { get; set; }
    }
}
