using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFilesService
    {
        public string GetFolder();
        string CheckIfDirectoryExistsAndCreateIfNot(Entities.Models.Document document, string category);
        Task StoreDocumentInFileSystem(IFormFile file, string path);
    }
}
