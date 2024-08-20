using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Models;

using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.DocumentStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class DocumentStatusService : IDocumentStatusService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public DocumentStatusService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task <IEnumerable<DocumentStatusDto>> 
            GetAllDocumentStatusesAsync(bool trackChanges)
        {
            var dc = await _repository.DocumentStatus.GetAllDocumentStatuses(trackChanges);
            var dcDto = _mapper.Map<IEnumerable<DocumentStatusDto>>(dc);
            return dcDto;
        }

        public async Task<DocumentStatusDto> GetDocumentStatusAsync(int id, bool trackChanges) 
        {
            var dc = await _repository.DocumentStatus.GetDocumentStatus(id, trackChanges);
            if (dc is null)
                throw new DocumentStatusNotFoundException(id);
            var dcDto = _mapper.Map<DocumentStatusDto>(dc);
            return dcDto;
        }
        //but here we will do create and other async

    }
}
