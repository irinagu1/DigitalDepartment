using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.DocumentCategories
{
    public record DocumentCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isEnable { get; set; }
        public int? ConnectedDocuments { get; set; }
    }
}
