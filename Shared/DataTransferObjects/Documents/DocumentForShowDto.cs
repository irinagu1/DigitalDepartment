using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public class DocumentForShowDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int DocumentStatusId { get; set; }
        public string DocumentStatusName { get; set; } = string.Empty;

        public int DocumentCategoryId { get; set; }
        public string DocumentCategoryName { get; set; } = string.Empty;


        public int LetterId { get; set; }
        public bool isArchived { get; set; }

        public DateTime DateCreation { get; set; }
        public bool isSigned { get; set; }
    }
}
