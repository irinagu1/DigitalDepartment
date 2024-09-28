using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ToCheck
{
    public class ToCheckDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? DocumentId { get; set; }

        public DateTime DateChecked { get; set; }
    }
}
