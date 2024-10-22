using Shared.DataTransferObjects.Position;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPositionService
    {
        Task<(IEnumerable<PositionDto> positions, MetaData metaData)>
          GetAllPositionsCategoriesAsync(
              PositionParameters positionParameters,
              bool trackChanges);

        Task<PositionDto> GetPositionByIdAsync(int id, bool trackChanges);

        Task<PositionDto> CreatePositionAsync(PositionForManipulationDto position);

        Task DeletePositionAsync(int positionId, bool trackChanges);

        Task UpdatePositionAsync(int positionId, PositionForUpdateDto  positionForUpdate, bool trackChanges);
    }
}
