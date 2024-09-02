using AutoMapper;
using Contracts.RepositoryCore;
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
    internal sealed class DocumentCategoryService : IDocumentCategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ICheckerService _checkerService;
        public DocumentCategoryService(IRepositoryManager repository, IMapper mapper, ICheckerService checkerService)
        {
            _repository = repository;
            _mapper = mapper;
            _checkerService = checkerService;
        }

        public async Task<(IEnumerable<DocumentCategoryDto> documentCategories, MetaData metaData)> 
            GetAllDocumentCategoriesAsync(DocumentCategoryParameters documentCategoryParameters, bool trackChanges)
        {
            var docCategoriesWithMetaData =
                await _repository.DocumentCategory.GetAllDocumentCategoriesAsync(
                documentCategoryParameters, trackChanges);
            var docCatDto = _mapper.Map<IEnumerable<DocumentCategoryDto>>(docCategoriesWithMetaData);
            return (documentCategories: docCatDto, metaData: docCategoriesWithMetaData.MetaData);
        }

        public async Task<DocumentCategoryDto> GetDocumentCategoryAsync(int Id, bool trackChanges)
        {
            var documentCategory = await _checkerService.GetDocumentCategoryEntityAndCheckiIfItExistsAsync(Id, trackChanges);
            var docCatDto = _mapper.Map<DocumentCategoryDto>(documentCategory);
            return docCatDto;
        }

        public async Task<DocumentCategoryDto> CreateDocumentCategoryAsync(DocumentCategoryForCreationDto documentCategory)
        {
            //check if the name is exist!!!! maybe throu filtering
            var documentCategoryEntity = _mapper.Map<DocumentCategory>(documentCategory);
            _repository.DocumentCategory.CreateDocumentCategory(documentCategoryEntity);
            await _repository.SaveAsync();
            var documentCategoryToReturn = _mapper.Map<DocumentCategoryDto>(documentCategoryEntity);
            return documentCategoryToReturn;
        }

        public async Task DeleteDocumentCategoryAsync(int documentCategoryId, bool trackChanges)
        {
            var documentCategory = await _checkerService.GetDocumentCategoryEntityAndCheckiIfItExistsAsync(documentCategoryId, trackChanges);
            _repository.DocumentCategory.DeleteDocumentCategory(documentCategory);
            await _repository.SaveAsync();
        }
        
        public async Task UpdateDocumentCategoryAsync(int documentCategoryId, DocumentCategoryForUpdateDto documentCategoryForUpdate, bool trackChanges)
        {
            var documentCategory = await _checkerService.GetDocumentCategoryEntityAndCheckiIfItExistsAsync(documentCategoryId, trackChanges);
            _mapper.Map(documentCategoryForUpdate, documentCategory);
            await _repository.SaveAsync();
        }
    }
}
