using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/documentstatuses")]
    [ApiController]
    public class DocumentStatusesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public DocumentStatusesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentStatuses()
        {
            var dc = await
                _service.DocumentStatusService.GetAllDocumentStatusesAsync(trackChanges: false);
            return Ok(dc);
        }

        [HttpGet("{id:int}", Name = "DocumentStatusById")]
        public async Task<IActionResult> GetDocumentStatus(int id) 
        {
            //new test comment 
            //for check sth on github
            var dc = await 
                _service.DocumentStatusService.GetDocumentStatusAsync(id, trackChanges: false);
            return Ok(dc);
        }

        //create
        [HttpPost(Name ="CreateDocumentStatus")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocumentStatus([FromBody] DocumentStatusForCreationDto documentStatus)
        {
            var createdDocumentStatus = await _service.DocumentStatusService.CreateDocumentStatusAsync(documentStatus);
            //search this
            return CreatedAtRoute("DocumentStatusById", new {id = createdDocumentStatus.Id}, createdDocumentStatus);
        }

        //update
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDocumentStatus(int id, [FromBody] DocumentStatusForUpdateDto documentStatus )
        {
            await _service.DocumentStatusService.UpdateDocumentStatusAsync(id, documentStatus, trackChanges: true);
            return NoContent();
        }

        //delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDocumentStatus(int id)
        {
            await _service.DocumentStatusService.DeleteDocumentStatusAsync(id, trackChanges: false);
            return NoContent();
        }
    }
}
