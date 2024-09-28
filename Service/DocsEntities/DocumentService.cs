using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
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

        public DocumentService(IRepositoryManager repository, IMapper mapper, ICheckerService checker, IFilesService filesService)
        {
            _repository = repository;
            _mapper = mapper;
            _checker = checker;
            _files = filesService;
        }

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
                    (uniqueRoles, toCheck, documentParameters, trackChanges);
               
            }
            else if (documentParameters.ForWho == "Лично мне")
            {
                document = await _repository.Document.GetAllDocumentsForUserAsync
                    (userId, toCheck, documentParameters, trackChanges);
            }

            var documentsDto = _mapper.Map<IEnumerable<DocumentDto>>(document);
            var allDocsWithParams = await GetAllDocumentsWithParametersNamesAsync
                (documents: documentsDto, metaData: document.MetaData, userId: userId);

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
                    DateSigned = isSigned != null ? isSigned.DateChecked : null
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
            var isSigned = await _repository.ToCheck.GetToCheckByUserAndDocumentIds(userId, documentId);
            return isSigned;
        }

        public async Task<(IEnumerable<DocumentDto> documents, MetaData metaData)> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges)
        {
            var docsWithMetaData = await _repository.Document.GetAllDocumentsAsync(documentParameters, trackChanges);
            var docsDto = _mapper.Map<IEnumerable<DocumentDto>>(docsWithMetaData);
            return (documents: docsDto, metaData: docsWithMetaData.MetaData);
        }

        public async Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto)
        {
            documentForCreationDto.Path = "D:\\CoreFiles" + "\\" + documentForCreationDto.Name;
            
            //мапим в ентити
            var documentEntity = _mapper.Map<Entities.Models.Document>(documentForCreationDto);
            //проверка существования категории, статуса, письма, что имя, файл, не пусто!! 
            await _checker.CheckDocumentParameters(documentEntity);
            //название категории
            var category = await _checker.GetDocumentCategoryEntityAndCheckiIfItExistsAsync(documentEntity.DocumentCategoryId, false);

            _repository.Document.CreateDocument(documentEntity);
            await _repository.SaveAsync();
            var documentToReturn = _mapper.Map<DocumentDto>(documentEntity);
            return documentToReturn;

            //создаем путь по которому положим файл
            /* var path =  _files.CheckIfDirectoryExistsAndCreateIfNot(documentEntity, category.Name);
             //путь + имя файла
             documentEntity.Path = path + documentForCreationDto.File.FileName;
             try 
             {
                 //кладем на диск
                  await _files.StoreDocumentInFileSystem(documentForCreationDto.File, path);
                 //создаем запись в базе данных
                 _repository.Document.CreateDocument(documentEntity);
             }
             catch(Exception e) 
             {
                 throw new Exception("Cannot store doc or create in db");
             }
             await _repository.SaveAsync();*/

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

        public DocumentDto ArchiveDocument(int id)
        {
            var document = _repository.Document.GetDocument(id, false);
            if (document is null)
                throw  new DocumentNotFoundException(id);
            document.isArchived = !document.isArchived;
            _repository.Document.UpdateDocument(document);
            _repository.Save();
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


    }
}
