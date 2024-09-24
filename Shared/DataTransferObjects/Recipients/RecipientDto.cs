using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Recipients
{
    public class RecipientDto
    {
        public int? Id { get; set; }
        public int LetterId { get; set; }
        public string Type { get; set; }
        public string TypeId { get; set; }
        public bool ToCheck { get; set; }
    }
}
