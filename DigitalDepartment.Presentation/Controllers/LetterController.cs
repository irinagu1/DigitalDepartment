using DigitalDepartment.Presentation.ActionFilters;
using Entities.Exceptions.NotFound;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
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

        [HttpGet]
        public async Task<IActionResult> GetRecipients([FromQuery] int letterId, int documentId)
        {
            var dtoToReturn = await _service.LetterService.GetRecipientsForReportByLetterId(letterId, documentId);

            return Ok(dtoToReturn);
        }


        [HttpGet("GetByCategories")]
        public async Task<IActionResult> GetAllRecipientsByCategories([FromQuery] int letterId)
        {
            var dtoToReturn = await _service.LetterService.GetRecipientsByLetterId(letterId);

            return Ok(dtoToReturn);
        }

        //create
        [HttpPost(Name = "CreateLetter")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocumentStatus([FromBody] LetterForCreationDto letterForCreationDto)
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            if (userId == null) 
                throw new Exception("cannot get userId");
            letterForCreationDto.AuthorId = userId;
            var createdLetter = await _service.LetterService.CreateLetterAsync(letterForCreationDto);
            return Ok(createdLetter);

            //   var createdDocumentStatus = await _service.DocumentStatusService.CreateDocumentStatusAsync(documentStatus);
            //search this
//            return Ok();
           // return CreatedAtRoute("DocumentStatusById", new { id = createdDocumentStatus.Id }, createdDocumentStatus);
        }
        

        [HttpPost("StoreRecipients")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> StoreRecipients([FromBody] RecipientsForCreationDto recipientsForCreation)
        {
            var createdRecipients = await _service.LetterService.StoreRecipients(recipientsForCreation);
            return Ok("success");

            //   var createdDocumentStatus = await _service.DocumentStatusService.CreateDocumentStatusAsync(documentStatus);
            //search this
            //            return Ok();
            // return CreatedAtRoute("DocumentStatusById", new { id = createdDocumentStatus.Id }, createdDocumentStatus);
        }


      


    }
}
