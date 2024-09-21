using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public int chunkSize;
        private string tempFolder;
        private readonly ResponseContext _responseData;

        public DocumentsController(IServiceManager service)
        {
            _service = service;
            chunkSize = 1048576 * 3;
            tempFolder = "D:\\CoreFiles";
            _responseData = new ResponseContext();
        }

        [HttpPost("UploadChunks")]
        public async Task<IActionResult> UploadChunks(string id, string fileName)
        {
            try
            {
                var chunkNumber = id;
                string newpath = Path.Combine(tempFolder + "/Temp", fileName + chunkNumber);
                using (FileStream fs = System.IO.File.Create(newpath))
                {
                    byte[] bytes = new byte[chunkSize];
                    int bytesRead = 0;
                    
                    while ((bytesRead = await Request.Body.ReadAsync(bytes, 0, bytes.Length)) > 0)
                    {
                        fs.Write(bytes, 0, bytesRead);
                    }
                }
            }
            catch (Exception ex)
            {
                _responseData.ErrorMessage = ex.Message;
                _responseData.IsSuccess = false;
            }
            return Ok(_responseData);
        }


        [HttpPost("UploadComplete")]
        public IActionResult UploadComplete(string fileName)
        {
            try
            {
                string tempPath = tempFolder + "/Temp";
                string newPath = Path.Combine(tempPath, fileName);
                string[] filePaths = Directory.GetFiles(tempPath).Where(p => p.Contains(fileName)).OrderBy(p => Int32.Parse(p.Replace(fileName, "$").Split('$')[1])).ToArray();
                foreach (string filePath in filePaths)
                {
                    MergeChunks(newPath, filePath);
                }
                System.IO.File.Move(Path.Combine(tempPath, fileName), Path.Combine(tempFolder, fileName));
            }
            catch (Exception ex)
            {
                _responseData.ErrorMessage = ex.Message;
                _responseData.IsSuccess = false;
            }
            return Ok(_responseData);
        }
        private static void MergeChunks(string chunk1, string chunk2)
        {
            FileStream fs1 = null;
            FileStream fs2 = null;
            try
            {
                fs1 = System.IO.File.Open(chunk1, FileMode.Append);
                fs2 = System.IO.File.Open(chunk2, FileMode.Open);
                byte[] fs2Content = new byte[fs2.Length];
                fs2.Read(fs2Content, 0, (int)fs2.Length);
                fs1.Write(fs2Content, 0, (int)fs2.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + ex.StackTrace);
            }
            finally
            {
                if (fs1 != null) fs1.Close();
                if (fs2 != null) fs2.Close();
                System.IO.File.Delete(chunk2);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments(
            [FromQuery] DocumentParameters documentParameters)
        {
            var pagedResult = await
                _service.DocumentService.GetAllDocumentsAsync(
                    documentParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.documents);
        }

        //create
        /*[HttpPost(Name = "CreateDocument")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentForCreationDto document)
        {
            var createdDocument = await _service.DocumentService.CreateDocumentAsync(document);
            return CreatedAtRoute("DocumentById", new { id = createdDocument.Id, createdDocument });
        }

            //  var createdDocumentCategory = await _service.DocumentCategoryService.CreateDocumentCategoryAsync(documentCategory);
            //search this
            //   return CreatedAtRoute("DocumentCategoryById", new { id = createdDocumentCategory.Id }, createdDocumentCategory);
            return Ok(createdDocument.Id);
        }*/



    }

    public class ResponseContext
    {
        public dynamic Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; }
    }
}
