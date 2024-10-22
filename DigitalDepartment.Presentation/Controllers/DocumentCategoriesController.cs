using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.RequestFeatures;
using System.Text.Json;

namespace DigitalDepartment.Presentation.Controllers
{

    [Route("api/documentcategories")]
    [ApiController]
  //  [Authorize]
    public class DocumentCategoriesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public DocumentCategoriesController(IServiceManager service)
        {
            _service = service;
        }

        /// <summary>
        /// gets the list of all document categories
        /// </summary>
        /// <param name="documentCategoryParameters"></param>
        /// <returns>The document categories list</returns>
        [HttpGet]
        //    [Authorize(Policy = "ReadDocumentStatuses")]
        public async Task<IActionResult> GetDocumentCategories(
            [FromQuery] DocumentCategoryParameters documentCategoryParameters)
        {
            var pagedResult = await
                _service.DocumentCategoryService.GetAllDocumentCategoriesAsync(
                    documentCategoryParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));
            
            return Ok(pagedResult.documentCategories);
        }

    
        [HttpGet("{id:int}", Name = "DocumentCategoryById")]
        //   [Authorize(Policy = "CreateDocumentStatus")]
        public async Task<IActionResult> GetDocumentCategory(int id)
        {
            var dc = await
                _service.DocumentCategoryService.GetDocumentCategoryAsync(id, trackChanges: false);
            return Ok(dc);
        }

        //create
        [HttpPost(Name = "CreateDocumentCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocumentCategory([FromBody] DocumentCategoryForCreationDto documentCategory)
        {
            var createdDocumentCategory = await _service.DocumentCategoryService.CreateDocumentCategoryAsync(documentCategory);
            //search this
            return CreatedAtRoute("DocumentCategoryById", new { id = createdDocumentCategory.Id }, createdDocumentCategory);
        }

        //update
        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDocumentCategory(int id, [FromBody] DocumentCategoryForUpdateDto documentCategory)
        {
            await _service.DocumentCategoryService.UpdateDocumentCategoryAsync(id, documentCategory, trackChanges: true);
            return NoContent();
        }

        //delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDocumentCategory(int id)
        {
            await _service.DocumentCategoryService.DeleteDocumentCategoryAsync(id, trackChanges: false);
            return NoContent();
        }
    }
}
