using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class DocumentService : IDocumentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public DocumentService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto)
        {
            var documentEntity = _mapper.Map<Document>(documentForCreationDto);
            _repository.Document.CreateDocument(documentEntity);
            await _repository.SaveAsync();
            var documentToReturn = _mapper.Map<DocumentDto>(documentEntity);
            return documentToReturn;

        }
    }
}
