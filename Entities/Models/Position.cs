using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Position
    {
        [Column("PositionId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Position name is a requred field")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Position isEnable is a requred field")]
        public bool? isEnable { get; set; }
    }
}
