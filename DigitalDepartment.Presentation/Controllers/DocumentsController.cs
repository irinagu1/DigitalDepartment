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


        public DocumentsController(IServiceManager service)
        {
            _service = service;
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
        [HttpPost(Name = "CreateDocument")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocument([FromForm] DocumentForCreationDto document)
        {
            var createdDocument = await _service.DocumentService.CreateDocumentAsync(document);
            return CreatedAtRoute("DocumentById", new { id = createdDocument.Id, createdDocument });
        }

        [HttpGet("{id:int}", Name = "DocumentById")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var dc = await _service.DocumentService.GetDocumentByIdAsync(id, trackChanges: false);
            return Ok(dc);
        }
    }
}
