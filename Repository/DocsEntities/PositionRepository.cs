using Contracts.DocsEntities;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using Repository.Extensions;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DocsEntities
{
    public class PositionRepository : RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(RepositoryContext repositoryContext)
          : base(repositoryContext)
        {
        }

        public void CreatePosition(Position position) =>
            Create(position);
        
        public void DeletePosition(Position position) =>
            Delete(position);

        public void UpdatePosition(Position position) => Update(position);

        public async Task<PagedList<Position>> GetAllPositionsAsync(PositionParameters positionParameters, bool trackChanges)
        {
            var positions = await FindAll(trackChanges)
                                  .FilterPositions(positionParameters)
                                  .OrderBy(dc => dc.Name)
                                  .Skip((positionParameters.PageNumber - 1) * positionParameters.PageSize)
                                  .Take(positionParameters.PageSize)
                                  .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<Position>(positions,
                                           count,
                                           positionParameters.PageNumber,
                                           positionParameters.PageSize);
        }

        public async Task<Position>
            GetPositionByIdAsync(int positionId, bool trackChanges) =>
            await FindByCondition(p => p.Id == positionId, trackChanges).SingleOrDefaultAsync();


    }
}
