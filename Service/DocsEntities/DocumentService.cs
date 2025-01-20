using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Service.Contracts.DocsEntities;
//using Service.ReportsManipulation;
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

        public bool DeleteDocument(int documentId)
        {
            _repository.Document.DeleteDocument(documentId);
            return true;
        }

        //CHECKEED



        public async Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)>
            GetDocumentsForShowAsync(string userId, DocumentParameters documentParameters, bool trackChanges)
        {
            bool toCheck = true;
            if (documentParameters.WhatType == "Для просмотра") toCheck = false;

            var document = new PagedList<Entities.Models.Document>();
            if (documentParameters.ForWho == "Общие")
            {
                HashSet<string> uniqueRoles = await _repository.User.GetUserRolesIds(userId);
                document = await _repository.Document.GetAllDocumentsForRolesAsync
                    (userId, uniqueRoles, toCheck, documentParameters, trackChanges);
               
            }
            else if (documentParameters.ForWho == "Лично мне")
            {
                document = await _repository.Document.GetAllDocumentsForUserAsync
                    (userId, toCheck, documentParameters, trackChanges);
            }

            var documentsDto = _mapper.Map<IEnumerable<DocumentDto>>(document);
            var allDocsWithParams = await GetAllDocumentsWithParametersNamesAsync
                (documents: documentsDto, metaData: document.MetaData, userId: userId);

            foreach(var el in allDocsWithParams.documents)
            {
                var author = _repository.User.GetUserByLetterIdAsync(el.LetterId);
                if (author is not null)
                    el.Author = author.FirstName + " " + author.SecondName + " " + author.LastName;
                else el.Author = "";
            }


            return (allDocsWithParams.documents, allDocsWithParams.metaData);
        }

        public async Task<(IEnumerable<DocumentForShowDto> documents, MetaData metaData)> 
            GetAllDocumentsWithParametersNamesAsync
            (IEnumerable<DocumentDto> documents, MetaData metaData, string userId)
        {
            List<DocumentForShowDto> documentsForShowDto = new List<DocumentForShowDto>();   
            foreach(var doc in documents)
            {
                var docCat = await _repository.DocumentCategory.GetDocumentCategoryAsync(doc.DocumentCategoryId, false);
                var docStat = await _repository.DocumentStatus.GetDocumentStatusAsync(doc.DocumentStatusId, false);
                var letter = await GetLetterById(doc.LetterId);
                var isSigned = await GetToCheckByUserAndDocumentId(userId, doc.Id);
                //var creationdate
                //var is signed
                var newDocForShowDto = new DocumentForShowDto()
                {
                    Id = doc.Id,
                    Name = doc.Name,
                    DocumentCategoryId = doc.DocumentCategoryId,
                    DocumentCategoryName = docCat.Name,
                    DocumentStatusId = doc.DocumentStatusId,
                    DocumentStatusName = docStat.Name,
                    isArchived = doc.isArchived,
                    LetterId = doc.LetterId,
                    DateCreation = letter.CreationDate.Value,
                    isSigned = isSigned != null ? true : false,
                    DateSigned = isSigned != null ? isSigned.DateChecked : null,
                    Author = doc.Author
                };
                documentsForShowDto.Add(newDocForShowDto);
            }

            return (documents: documentsForShowDto, metaData);
        }

        private async Task<Letter> GetLetterById(int id)
        {
            var letter = await _repository.Letter.GetLetterById(id);
            return letter;
        }

        private async Task<ToCheck> GetToCheckByUserAndDocumentId(string userId, int documentId)
        {
            //    var isSigned = await _repository.ToCheck.GetToCheckByUserAndDocumentIds(userId, documentId);

            var isSigned = await _repository.ToCheck
                .GetToCheckByUserAndVersionId(userId, 13);
            return isSigned;
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
