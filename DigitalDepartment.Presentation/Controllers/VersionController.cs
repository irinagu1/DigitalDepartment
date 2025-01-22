using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.DocumentVersion;
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
    [Route("api/versions")]
    [ApiController]
    [Authorize]
    public class VersionController : ControllerBase
    {
        private readonly IServiceManager _service;

        public VersionController(IServiceManager service)
        {
            _service = service;
         
        }

        [HttpGet]
        [Route("nameForDownload")]
        public async Task<IActionResult> NameForDownload(long versionId)
        {
            var name = await _service.DocumentVersionService.NameForDownload(versionId);
            return Ok(name);
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadFile(long versionId)
        {
            var path = await _service.DocumentVersionService.DownloadFile(versionId);
            if (path == "error")
                return NotFound("not found path in directory");
            else
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, "application/octet-stream", path);
            }
        }


        [HttpGet("forReport")]
        public async Task<IActionResult> GetRecipientsForReport
           ([FromQuery] long versionId)
        {
            var result = await _service.DocumentVersionService
                .GetRecipientsForReportByVersionIs(versionId);
            return Ok(result);
        }

        [HttpGet("byId")]
        public async Task<IActionResult> GetVersionById
            ([FromQuery] long versionId)
        {
            var version = await _service.DocumentVersionService.
                GetVersionById(versionId);
            return Ok(version);
        }

        [HttpGet("byDocumentId")]
        public async Task<IActionResult> GetAllVersionsByDocumentId
           ([FromQuery] int documentId)
        {
            var versions =await _service.DocumentVersionService.
                GetAllVersionsByDocumentId(documentId);
            return Ok(versions);
        }

        [HttpGet("forUser")]
        public IActionResult GetAllVersionsWhereAuthorIsUser
           ([FromQuery] VersionParameters versionParameters)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var pagedResult = _service.DocumentVersionService
                .GetAllVersionsWhereAuthorIsUser(userId, versionParameters);
            Response.Headers.Add("X-Pagination",
               JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.versions);
        }

        [HttpGet("forShowByUserId")]
        public async Task<IActionResult> GetAllVersionsByUserId
         ([FromQuery] VersionParameters versionParameters)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var pagedResult = await _service.DocumentVersionService
                .GetAllVersionsByUser(userId, versionParameters);
            Response.Headers.Add("X-Pagination",
               JsonSerializer.Serialize(pagedResult.metaData));
          
            return Ok(pagedResult.versions);
        }

        [HttpGet("forShowByRoles")]
        public async Task<IActionResult> GetAllVersionsByRoles
         ([FromQuery] VersionParameters versionParameters)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var pagedResult = await _service.DocumentVersionService.GetAllVersionsByRoles(
                userId, versionParameters);
             Response.Headers.Add("X-Pagination",
               JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.versions);
        }

        [HttpPost("createAnotherVersion")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAnotherVersion
           ([FromBody] DocumentVersionForCreationDto versionDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            await _service.DocumentVersionService.CreateAnotherVersionEntity(
                versionDto.DocumentId,
                versionDto.Path,
                versionDto.Message,
                userId);
            return Ok();
        }

        [HttpPost("SignVersion")]
        public async Task<IActionResult> SignDocument(string versionId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId").Value.ToString();
            var toCheckDto = await _service.ToCheckService.Create(userId, long.Parse(versionId));

            return NoContent();
        }

        [HttpDelete("DeleteVersion")]
        public async Task<IActionResult> DeleteVersion(long versionId)
        {
            await _service.DocumentVersionService.DeleteDocumentVersion(versionId);
                return Ok();
        }
    }

}
