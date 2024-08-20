using AutoMapper;
using Contracts.RepositoryCore;
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

        public IEnumerable<DocumentStatusDto> 
            GetAllDocumentStatuses(bool trackChanges)
        {
            var dc = _repository.DocumentStatus.GetAllDocumentStatuses(trackChanges);
            var dcDto = _mapper.Map<IEnumerable<DocumentStatusDto>>(dc);
            return dcDto;
        }

        public DocumentStatusDto GetDocumentStatus(int id, bool trackChanges) 
        {
            var dc = _repository.DocumentStatus.GetDocumentStatus(id, trackChanges);
            //check null
            var dcDto = _mapper.Map<DocumentStatusDto>(dc);
            return dcDto;
        }

    }
}
