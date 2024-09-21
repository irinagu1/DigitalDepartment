using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{

    [Route("api/letters")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LetterController(IServiceManager service)
        {
            _service = service;
        }

        //create
        [HttpPost(Name = "CreateLetter")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocumentStatus([FromBody] LetterForCreationDto letterForCreationDto)
        {
            
            var createdDocumentStatus = await _service.DocumentStatusService.CreateDocumentStatusAsync(documentStatus);
            //search this
          //  return Ok()
            return CreatedAtRoute("DocumentStatusById", new { id = createdDocumentStatus.Id }, createdDocumentStatus);
        }
    }
}
