using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.ToCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class ToCheckService : IToCheckService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public ToCheckService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToCheckDto> Create(string userId, long versionId)
        {
            var dto = CreateDto(userId, versionId);    
            var entity = _mapper.Map<ToCheck>(dto);
            _repository.ToCheck.CreateToCheck(entity);
            await _repository.SaveAsync();
            var ToCheckDto = _mapper.Map<ToCheckDto>(entity);
            return ToCheckDto;
        }

        ToCheckForCreateDto CreateDto(string userId, long versionId)
        {
            return new ToCheckForCreateDto { 
                UserId = userId, 
                VersionId = versionId,
                DateChecked = DateTime.Now
            };
        }

      
    }
}
