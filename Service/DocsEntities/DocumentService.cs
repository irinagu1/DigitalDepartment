using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
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

        public async Task<(IEnumerable<DocumentDto> documents, MetaData metaData)> GetAllDocumentsAsync(DocumentParameters documentParameters, bool trackChanges)
        {
            var docsWithMetaData = await _repository.Document.GetAllDocumentsAsync(documentParameters, trackChanges);
            var docsDto = _mapper.Map<IEnumerable<DocumentDto>>(docsWithMetaData);
            return (documents: docsDto, metaData: docsWithMetaData.MetaData);
        }

        public async Task<DocumentDto> CreateDocumentAsync(DocumentForCreationDto documentForCreationDto)
        {
            //мапим в ентити
            var documentEntity = _mapper.Map<Entities.Models.Document>(documentForCreationDto);
            //проверка существования категории, статуса, письма, что имя, файл, не пусто!! 
            await _checker.CheckDocumentParameters(documentEntity, documentForCreationDto.File);
            //название категории
            var category = await _checker.GetDocumentCategoryEntityAndCheckiIfItExistsAsync(documentEntity.DocumentCategoryId, false);
            //создаем путь по которому положим файл
            var path =  _files.CheckIfDirectoryExistsAndCreateIfNot(documentEntity, category.Name);
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
            await _repository.SaveAsync();
            var documentToReturn = _mapper.Map<DocumentDto>(documentEntity);
            return documentToReturn;
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
    }
}
