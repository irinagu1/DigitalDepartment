using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/documents")]
    [ApiController]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DocumentsController(IServiceManager service)
        {
            _service = service;

        }

        [HttpGet("byIdForVersion")]
        public async Task<IActionResult> GetDocumentByIdForVersion(
         [FromQuery] int documentId)
        {
            var dto = await _service.DocumentService
                .GetDocumentByIdForVersion(documentId);
            return Ok(dto);
        }

        [HttpPost("CreateDocument")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentForCreationDto document)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var createdDocument = await _service.DocumentService.CreateDocumentAsync(document, userId);
            return Ok(createdDocument.Id);
        }

        [HttpGet("all")]
        public IActionResult GetAllDocuments(
           [FromQuery] DocumentShowParameters documentParameters)
        {
            var pagedResult = _service.DocumentService.GetAllDocumentsForShowAsync(
                    documentParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.documents);
        }

        [HttpPut("ArchiveDocument")]
        public async Task<IActionResult> ArchiveDocument(string documentId)
        {
            var documentDto = _service.DocumentService.ArchiveDocument(int.Parse(documentId));
            if (documentDto is not null)
                return NoContent();
            throw new Exception("document is null");
        }


        [HttpDelete("DeleteDocument")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            var result = await _service.DocumentService.DeleteDocument(documentId);
            if (result)
                return NoContent();
            throw new Exception("cannot delete document");
        }

        [HttpPut("UpdateDocument")]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentForUpdateDto documentForUpdateDto)
        {
            var documentDto = _service.DocumentService.UpdateDocument(documentForUpdateDto);
            if (documentDto is not null)
                return NoContent();
            throw new Exception("document is null");
        }

        [HttpPost("uploadfile")]
        public async Task<IActionResult> UploadFile(IFormFile file, IFormCollection formCollection)

        {
            var description = formCollection["description"];
            if (file == null)
            {
                return BadRequest("No file was provided.");
            }
            var baseFolder = _service.DocumentVersionService.returnBaseFolder();
            bool exists = System.IO.Directory.Exists(baseFolder);
            if (!exists)
                System.IO.Directory.CreateDirectory(baseFolder);


            var filePath = Path.Combine(baseFolder, description);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok("File uploaded successfully.");
        }


    }

}