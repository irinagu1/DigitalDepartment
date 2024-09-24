using Entities.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Recipient
    {
        [Column("RecipientId")]
        public int Id { get; set; }

        
        [ForeignKey(nameof(Letter))]
        public int LetterId { get; set; }
        public Letter? Letter { get; set; }

        public string Type { get; set; }
        public string TypeId { get; set; }
        public bool ToCheck { get; set; }
    }
}
