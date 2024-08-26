using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        //create
        [HttpPost(Name = "CreateDocument")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateDocumentCategory([FromBody] DocumentCategoryForCreationDto documentCategory)
        {
            //проверка что существует такой статус, такая категория, такое письмо, такой отправитель и что все ок в принципе
            
           //идем на уровень сервиса и можем вернуть ссылку

            var createdDocumentCategory = await _service.DocumentCategoryService.CreateDocumentCategoryAsync(documentCategory);
            //search this
            return CreatedAtRoute("DocumentCategoryById", new { id = createdDocumentCategory.Id }, createdDocumentCategory);
        }
    }
}
