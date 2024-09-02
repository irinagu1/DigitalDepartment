using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Letters
{
    public class LetterDto
    {
        public int Id { get; init; }
        
        public string? Text {  get; init; }

        public DateTime? CreationDate { get; set; }

    }
}
