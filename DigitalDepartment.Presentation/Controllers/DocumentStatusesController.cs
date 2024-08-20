using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDocumentStatus(int id) 
        {
            //new test comment 
            //for check sth on github
            var dc = await 
                _service.DocumentStatusService.GetDocumentStatusAsync(id, trackChanges: false);
            return Ok(dc);
        }
    }
}
