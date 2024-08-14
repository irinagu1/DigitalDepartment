using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class DocumentCategory
    {
        [Column("DocumentCategoryId")]
        public int Id { get; set; }
 
        [Required(ErrorMessage ="Document category name is a requred field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Document category isEnable is a requred field")]
        public bool? isEnable { get; set; }

        //  public ICollection<Docs>? Docs { get; set; }
    }
}
