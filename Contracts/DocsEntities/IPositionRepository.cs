using Entities.Models;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DocsEntities
{
    public interface IPositionRepository
    {
        Task<PagedList<Position>> GetAllPositionsAsync(
         PositionParameters positionParameters, bool trackChanges);
        Task<Position> GetPositionByIdAsync(int positionId, bool trackChanges);
        void CreatePosition(Position position);
        void DeletePosition(Position position);
    }
}
