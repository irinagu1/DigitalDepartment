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
            throw new Exception("Myex");
            var dc = _service.DocumentStatusService.GetAllDocumentStatuses(trackChanges: false);
            return Ok(dc);
        }
    }
}
