using Contracts.RepositoryCore;
using Entities.Exceptions;
using Entities.Exceptions.NotFound;
using Entities.Exceptions.NotSingle;
using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Shared.DataTransferObjects.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class CheckerService : ICheckerService
    {
        private readonly IRepositoryManager _repository;
        public CheckerService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<DocumentCategory> GetDocumentCategoryEntityAndCheckiIfItExistsAsync(int id, bool trackChanges)
        {
            var documentCategory = await _repository.DocumentCategory.GetDocumentCategoryAsync(id, trackChanges);
            if(documentCategory is null)
                throw new DocumentCategoryNotFoundException(id);
            return documentCategory;
        }

        public async Task<DocumentStatus> GetDocumentStatusEntityAndCheckIfItExistsAsync(int id, bool trackChanges)
        {
            var documentStatus = await _repository.DocumentStatus.GetDocumentStatusAsync(id, trackChanges);
            if (documentStatus is null)
                throw new DocumentStatusNotFoundException(id);
            return documentStatus;
        }

        public async Task<Letter> GetLetterAndCheckIfItExist(int id, bool trackChanges)
        {
            //implement 
            return new Letter();
        }



        public async Task CheckDocumentParameters(Entities.Models.Document documentEntity)
        {
            await GetDocumentStatusEntityAndCheckIfItExistsAsync(documentEntity.DocumentStatusId, false);
            await GetDocumentCategoryEntityAndCheckiIfItExistsAsync(documentEntity.DocumentCategoryId, false);
            await CheckPathOriginalityAndEmpty(documentEntity.Path, false);
        }

        public async Task CheckPathOriginalityAndEmpty(string? path, bool trackChanges)
        {
            if (path is null)
                throw new Exception("Document path is empty");
            var document = await _repository.Document.GetDocumentbyPathAsync(path, trackChanges);
            if (document is not null)
                throw new DocumentNotSingleException(path);
        }

        public async Task<Role> GetRoleEntityAndCheckIdExistsAsync(string id, bool trackChanges)
        {
            var roleEntity = await _repository.Role.GetRoleAsync(id, trackChanges);
            if (roleEntity is null)
                throw new RoleNotFoundException(id);
            return roleEntity;
        }

        public User GetUserEntityAndCheckItExists(string id, bool trackChanges)
        {
            var userEntity = _repository.User.GetUserById(id);
            if (userEntity is null)
                throw new UserNotFound(id);
            return userEntity;
        }

        public async Task<Position> GetPositionEntityAndCheckIfItExistsAsync(int id, bool trackChanges)
        {
            var entity = 
                await _repository.Position.GetPositionByIdAsync(id, trackChanges);
            if(entity is null)
                throw new PositionnotFoundException(id);
            return entity;
        }
    }
}
