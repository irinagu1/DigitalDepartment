using Entities.Models;
using Entities.Models.Auth;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICheckerService
    {
        Task<Position> GetPositionEntityAndCheckIfItExistsAsync(int id, bool trackChanges);
        Task<DocumentStatus> GetDocumentStatusEntityAndCheckIfItExistsAsync(int id, bool trackChanges);
        Task<DocumentCategory> GetDocumentCategoryEntityAndCheckiIfItExistsAsync(int id, bool trackChanges);
        Task<Role> GetRoleEntityAndCheckIdExistsAsync(string id, bool trackChanges);
        Task CheckDocumentParameters(Document documentEntity);
        User GetUserEntityAndCheckItExists(string id, bool trackChanges);
        void CheckIfPathIsEmpty(string? path);

    }
}
