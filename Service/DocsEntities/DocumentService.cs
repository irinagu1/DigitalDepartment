using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class DocumentService : IDocumentService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checker;
        private readonly IFilesService _files;
        private readonly IDocumentVersionService _versions;
        public DocumentService(
            IRepositoryManager repository, 
            IMapper mapper, 
            ICheckerService checker, 
            IFilesService filesService,
            IDocumentVersionService versionService)
        {
            _repository = repository;
            _mapper = mapper;
            _checker = checker;
            _files = filesService;
            _versions = versionService;
        }

        public async Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto,
            string authorId)
        {
            var documentDto = await CreateDocumentEnity(documentForCreationDto);
            await _versions.CreateVersionEntity(
                1,
                documentForCreationDto.Path,
                documentForCreationDto.Message,
                documentDto, 
                authorId
                );

            return documentDto;
        }

        public async Task<DocumentDto> CreateDocumentEnity(DocumentForCreationDto documentForCreationDto)
        {
            var documentEntity = _mapper.Map<Entities.Models.Document>(documentForCreationDto);
            await _checker.CheckDocumentParameters(documentEntity);
            _repository.Document.CreateDocument(documentEntity);
            await _repository.SaveAsync();
            var documentDto = _mapper.Map<DocumentDto>(documentEntity);
            return documentDto;
        }

        public async Task<DocumentForVersion> GetDocumentByIdForVersion(int id)
        {
            var documentEntity = await _checker.GetDocumentEntityAndCheckIfExistsAsync(id);
            var status = await 
                _checker.GetDocumentStatusEntityAndCheckIfItExistsAsync
                (documentEntity.DocumentStatusId, false);
            var category = await
               _checker.GetDocumentCategoryEntityAndCheckiIfItExistsAsync
               (documentEntity.DocumentCategoryId, false);

            var documentForVersion = new DocumentForVersion()
            {
                Id = documentEntity.Id,
                Name = documentEntity.Name,
                DocumentStatusId = documentEntity.DocumentStatusId,
                DocumentStatusName = status.Name,
                DocumentCategoryId = documentEntity.DocumentCategoryId,
                DocumentCategoryName = category.Name,
                LetterId = documentEntity.LetterId
            };
            return documentForVersion;
        }

        public (List<DocumentShowDto> documents, MetaData metaData) 
            GetAllDocumentsForShowAsync
            (DocumentShowParameters documentParameters, bool trackChanges)
        {
            var documents = _repository.Document.GetAllDocumentsForShowAsync
                (documentParameters, false);
            return (documents, documents.MetaData);
        }

        public DocumentDto ArchiveDocument(int id)
        {
            var document = _repository.Document.GetDocument(id, false);
            if (document is null)
                throw new DocumentNotFoundException(id);
            document.isArchived = !document.isArchived;
            _repository.Document.UpdateDocument(document);
            _repository.Save();
            var documentDto = _mapper.Map<DocumentDto>(document);
            return documentDto;
        }

        public async Task<bool> DeleteDocument(int documentId)
        {
            var versions = _repository.DocumentVersion.GetAllVersionsByDocumentId(documentId).ToList();
            for(int i=0; i< versions.Count(); i++)
            {
                await _versions.DeleteFileWithVersion(versions[i].Id);
            }
            _repository.Document.DeleteDocument(documentId);
            return true;
        }

        public async Task<(IEnumerable<DocumentDto> documents, MetaData metaData)> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges)
        {
            var docsWithMetaData = await _repository.Document.GetAllDocumentsAsync(documentParameters, trackChanges);
            var docsDto = _mapper.Map<IEnumerable<DocumentDto>>(docsWithMetaData);
            return (documents: docsDto, metaData: docsWithMetaData.MetaData);
        }

      
        public async Task<DocumentDto> GetDocumentByIdAsync(int id, bool trackChanges)
        {
            var document = await _repository.Document.GetDocumentAsync(id, trackChanges);
            var documentDto = _mapper.Map<DocumentDto>(document);
            return documentDto;
        }

        public async Task<DocumentDto> GetDocumentByPathAsync(string path, bool trackChanges)
        {
            var document = await _repository.Document.GetDocumentbyPathAsync(path, trackChanges);
            var documentDto = _mapper.Map<DocumentDto>(document);
            return documentDto;
        }

        public DocumentDto UpdateDocument(DocumentForUpdateDto documentForUpdateDto)
        {
            var documentEntity = _repository.Document.GetDocument(documentForUpdateDto.Id, false);
            if (documentForUpdateDto.DocumentStatusId is not null)
            {
                documentEntity.DocumentStatusId = documentForUpdateDto.DocumentStatusId.Value;
            }
            if(documentForUpdateDto.DocumentCategoryId is not null)
                documentEntity.DocumentCategoryId = documentForUpdateDto.DocumentCategoryId.Value;
            _repository.Document.UpdateDocument(documentEntity);
            _repository.Save();

            return _mapper.Map<DocumentDto>(documentEntity);
        }

        public int AmountOfConnectedDocumentsByCategoryId(int categoryId)
        {
            var count = _repository.Document.AmountOfConnectedDocumentsByCategoryId(categoryId);
            return count;
        }

    
    }
}
