﻿using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.RequestFeatures;
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
        private readonly ICheckerService _checkerService;
        public DocumentStatusService(IRepositoryManager repository, IMapper mapper, ICheckerService checker)
        {
            _repository = repository;
            _mapper = mapper;
            _checkerService = checker;
        }

        public async Task <(IEnumerable<DocumentStatusDto> documentStatuses, 
                            MetaData metaData)> 
            GetAllDocumentStatusesAsync(
                DocumentStatusParameters documentStatusParameters, 
                bool trackChanges)
        {
            var dcWithMetaData =
                await _repository.DocumentStatus.GetAllDocumentStatusesAsync(
                documentStatusParameters, trackChanges);
            var dcDto = _mapper.Map<IEnumerable<DocumentStatusDto>>(dcWithMetaData);
            return (documentStatuses: GetConnectedDocuments(dcDto), metaData: dcWithMetaData.MetaData);
        }

        public async Task<DocumentStatusDto> GetDocumentStatusAsync(int id, bool trackChanges) 
        {
            var documentStatus = await _checkerService.GetDocumentStatusEntityAndCheckIfItExistsAsync(id, trackChanges);
            var dcDto = _mapper.Map<DocumentStatusDto>(documentStatus);
            return dcDto;
        }
        IEnumerable<DocumentStatusDto> GetConnectedDocuments(IEnumerable<DocumentStatusDto> list)
        {
            foreach (var el in list)
            {
                var count = _repository.Document.AmountOfConnectedDocumentsByStatusId(el.Id);
                el.ConnectedDocuments = count;
            }
            return list;
        }
        public async Task<DocumentStatusDto> CreateDocumentStatusAsync(DocumentStatusForCreationDto documentStatus)
        {
            //check if the name is exist!!!! maybe throu filtering
            var documentStatusEntity = _mapper.Map<DocumentStatus>(documentStatus);
            _repository.DocumentStatus.CreateDocumentStatus(documentStatusEntity);
            await _repository.SaveAsync();
            var documentStatusToReturn = _mapper.Map<DocumentStatusDto>(documentStatusEntity);
            return documentStatusToReturn;
        }

        public async Task DeleteDocumentStatusAsync(int documentStatusId, bool trackChanges)
        {
            var documentStatus = await _checkerService.GetDocumentStatusEntityAndCheckIfItExistsAsync(documentStatusId, trackChanges);
            _repository.DocumentStatus.DeleteDocumentStatus(documentStatus);
            await _repository.SaveAsync();
        }
        public async Task UpdateDocumentStatusAsync(int documentStatusId, DocumentStatusForUpdateDto documentStatusForUpdate, bool trackChanges)
        {
            var documentStatus = await _checkerService.GetDocumentStatusEntityAndCheckIfItExistsAsync(documentStatusId, trackChanges);
            _mapper.Map(documentStatusForUpdate, documentStatus);
            await _repository.SaveAsync();
        }
    }
}
