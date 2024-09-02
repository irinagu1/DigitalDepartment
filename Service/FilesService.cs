using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Service.Contracts;

namespace Service
{
    public class FilesService : IFilesService
    {
        private readonly string baseFolder;

        public FilesService(string baseFolder)
        {
            this.baseFolder = baseFolder;
        }

        public string GetFolder() => baseFolder;

        public string CheckIfDirectoryExistsAndCreateIfNot(Entities.Models.Document document, string category)
        {
            try
            {
                string dir = @$"{GetFolder()}\{category}";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task StoreDocumentInFileSystem(IFormFile file, string path)
        {
            try
            {
                path = path + @"\file.txt";
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
