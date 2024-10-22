using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryPositionExtension
    {
        public static IQueryable<Position> FilterPositions
          (this IQueryable<Position> positions, 
            PositionParameters positionParameters)
        {
            if (positionParameters.isEnable is null)
                return positions;
            return positions.Where(p => 
                p.isEnable == positionParameters.isEnable);
        }
    }
}
