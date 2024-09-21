using Entities.Models.Auth;
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


        [ForeignKey(nameof(User))]
        public string AuthorId { get; set; }
        public User? Author { get; set; }

        public string? Text { get; set; }

        public DateTime? CreationDate { get; set; }
        
        public ICollection<Document>? Documents { get; set; }
    }
}
