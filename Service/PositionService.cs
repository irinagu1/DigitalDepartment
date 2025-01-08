using AutoMapper;
using Contracts.RepositoryCore;
using Service.Contracts;
using Shared.DataTransferObjects.Position;
using Shared.RequestFeatures;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.DocumentCategories;

namespace Service
{
    internal sealed class PositionService : IPositionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checkerService;
        public PositionService(IRepositoryManager repository, 
            IMapper mapper, ICheckerService checkerService)
        {
            _repository = repository;
            _mapper = mapper;
            _checkerService = checkerService;
        }

        public async Task<PositionDto> CreatePositionAsync(PositionForManipulationDto position)
        {
           var positionEntity = _mapper.Map<Position>(position);
           _repository.Position.CreatePosition(positionEntity);
           await _repository.SaveAsync();
           var dtoToReturn = _mapper.Map<PositionDto>(positionEntity);
           return dtoToReturn;
        }

        public async Task DeletePositionAsync(int positionId, bool trackChanges)
        {
            var position = 
                await _checkerService.GetPositionEntityAndCheckIfItExistsAsync
                (positionId, trackChanges);

            _repository.Position.DeletePosition(position);
            await _repository.SaveAsync();
        }
        public async Task UpdatePosition(int positionId, PositionForUpdateDto positionForUpdate, bool trackChanges)
        {
            var position =
               await _checkerService.GetPositionEntityAndCheckIfItExistsAsync
               (positionId, trackChanges);
            _mapper.Map(positionForUpdate, position);
            _repository.Position.UpdatePosition(position);
            _repository.Save();
        }

        public async Task<PositionDto> GetPositionByIdAsync(int id, bool trackChanges)
        {
            var position =
                await _checkerService.GetPositionEntityAndCheckIfItExistsAsync
                (id, trackChanges);
            var dto = _mapper.Map<PositionDto>(position);
            return dto;
        }

        public async Task<(IEnumerable<PositionDto> positions, MetaData metaData)> 
            GetAllPositionsAsync
            (PositionParameters positionParameters, bool trackChanges)
        {
            var positionsWithMetaData = 
                await _repository.Position.GetAllPositionsAsync
                (positionParameters, trackChanges);

            var dto = _mapper.Map<IEnumerable<PositionDto>>(positionsWithMetaData);
            return (positions: GetConnectedUsers(dto),
                    metaData: positionsWithMetaData.MetaData);
        }

        IEnumerable<PositionDto> GetConnectedUsers(IEnumerable<PositionDto> list)
        {
            foreach (var el in list)
            {
                var count = 
                    _repository.User.AmountOfConnectedUsersByPositionId(el.Id);
                el.ConnectedUsers = count;
            }
            return list;
        }


    }
}
