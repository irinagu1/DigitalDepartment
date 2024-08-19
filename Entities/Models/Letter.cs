using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Letter
    {
        [Column("LetterId")]
        public int Id { get; set; }

        //author id
        public string? Text { get; set; }

        public DateTime? CreationDate { get; set; }
        
        public ICollection<Document>? Documents { get; set; }
    }
}
