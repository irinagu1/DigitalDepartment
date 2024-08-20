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
        public IActionResult GetDocumentStatuses()
        {
            var dc = _service.DocumentStatusService.GetAllDocumentStatuses(trackChanges: false);
            return Ok(dc);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetDocumentStatuss(int id) 
        {
            var dc = _service.DocumentStatusService.GetDocumentStatus(id, trackChanges: false);
            return Ok(dc);
        }
    }
}
