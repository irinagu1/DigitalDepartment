using DigitalDepartment.Presentation.ActionFilters;
using Entities.Exceptions.NotFound;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentStatuses;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet("CheckAfterUploading")]
        public async Task<IActionResult> CheckAfterUploading([FromQuery] int letterId)
        {
            var result = await _service.LetterService
                .CheckAfterUploadingAndClearIfUncorrect(letterId);
            if (result)
                return Ok(result);
            else return BadRequest();
        }

        [HttpGet("CreateReport")]
        public async Task<IActionResult> CreateReport([FromQuery] long versionId )
        {
            await _service.LetterService.CreateReport(versionId);
            return Ok();
        }
       
        [HttpGet("DownloadReport")]
        public async Task<IActionResult> DownloadReport([FromQuery] long versionId)
        {
            var baseFolder = _service.DocumentVersionService.returnBaseFolderReport();
            var versionPath = await _service.DocumentVersionService.returnVersionPath(versionId);
            var filePath = Path.Combine(baseFolder, versionPath);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", filePath);
        }

        [HttpGet("DeleteReport")]
        public async Task<IActionResult> Delete([FromQuery] long versionId)
        {
            var baseFolder = _service.DocumentVersionService.returnBaseFolderReport();
            var versionPath = await _service.DocumentVersionService.returnVersionPath(versionId);
            var filePath = Path.Combine(baseFolder, versionPath);

            System.IO.File.Delete(filePath);
            return Ok();
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
        public async Task<IActionResult> CreateLetter([FromBody] LetterForCreationDto letterForCreationDto)
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
