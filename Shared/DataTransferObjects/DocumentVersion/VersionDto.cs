using Shared.DataTransferObjects.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.DocumentVersion
{
    public record VersionDto
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public bool isLast { get; set; }
        public int DocumentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Message { get; set; } = "";
        public string AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public bool? canDelete { get; set; }
    }
}
