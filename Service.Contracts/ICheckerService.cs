using Entities.Models;
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
        Task<DocumentStatus> GetDocumentStatusEntityAndCheckIfItExistsAsync(int id, bool trackChanges);
        Task<DocumentCategory> GetDocumentCategoryEntityAndCheckiIfItExistsAsync(int id, bool trackChanges);
        Task CheckDocumentParameters(Document documentEntity);
    }
}
