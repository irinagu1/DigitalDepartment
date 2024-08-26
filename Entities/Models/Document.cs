using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Document
    {
        [Column("DocumentId")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Document name is a requred field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Document path is a requred field")]
        public string? Path { get; set; }

     
        [ForeignKey(nameof(DocumentStatus))]
        public int DocumentStatusId { get; set; }
        public DocumentStatus? DocumentStatus { get; set; }


        [ForeignKey(nameof(DocumentCategory))]
        public int DocumentCategoryId { get; set; }
        public DocumentCategory? DocumentCategory { get; set; }


        [ForeignKey(nameof(Letter))]
        public int LetterId { get; set; }
        public Letter? Letter { get; set; }
    }
}
