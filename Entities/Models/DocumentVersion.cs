using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class DocumentVersion
    {
        [Column("DocumentVersionId")]
        public long Id { get; set; }

        public long Number { get; set; }

        public bool isLast { get; set; }

        [ForeignKey(nameof(Document))]
        public int DocumentId { get; set; }
        public Document? Document { get; set; }

       
        [Required(ErrorMessage = "Document path is a requred field")]
        public string? Path { get; set; }

        public DateTime? CreationDate { get; set; }

        [ForeignKey(nameof(User))]
        public string? AuthorId { get;set; }
        public User? Author { get; set; }

        public string Message { get; set; } = "";

    }
}
