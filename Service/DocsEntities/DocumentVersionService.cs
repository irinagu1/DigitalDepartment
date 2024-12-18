using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    public sealed class DocumentVersionService : IDocumentVersionService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checker;

        public DocumentVersionService(
            IRepositoryManager repository, 
            IMapper mapper, 
            ICheckerService checker)
        {
            _repository = repository;
            _mapper = mapper;
            _checker = checker;
        }

        public async Task CreateVersionEntity(int number, string path, DocumentDto documentDto)
        {
            _checker.CheckIfPathIsEmpty(path);
            var versionDto = GetVersionForCreationDto(
                1,
                documentDto.Id,
                path,
                ""
                );

            var versionEntity = _mapper.Map<DocumentVersion>(versionDto);
            _repository.DocumentVersion.CreateDocumentVersion(versionEntity);

            await _repository.SaveAsync();
        }

        public DocumentVersionForCreationDto GetVersionForCreationDto(
            int number,
            int documentId,
            string path,
            string message
            )
        {
            var dto = new DocumentVersionForCreationDto()
            {
                Number = 1,
                isLast = true,
                DocumentId = documentId,
                Path = path,
                CreationDate = DateTime.Now,
                Message = message
            };
            return dto;
        }

    }
}
