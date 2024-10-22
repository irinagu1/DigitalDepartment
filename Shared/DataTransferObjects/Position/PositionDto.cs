using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Position
{
    public record PositionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool isEnable { get; set; }
        public int? ConnectedUsers { get; set; }
    }
}
