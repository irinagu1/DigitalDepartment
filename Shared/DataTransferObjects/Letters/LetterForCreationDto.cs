using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Letters
{
    public record LetterForCreationDto
    {
        public string? Text { get; init; } = "Text";
        public string? AuthorId { get; init; }
        public DateTime? CreationDate { get; set; } = DateTime.Now;

    }
}
