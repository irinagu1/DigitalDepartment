using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public class DocumentShowDto
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public bool isArchived { get; set; }
        public int LetterId { get; set; }
        public DateTime LetterCreationDate { get; set; }
        public string LetterAuthorId { get; set; }
        public string LetterAuthorFullName{ get; set; }
        public bool? CanDelete { get; set; }
    }
}
