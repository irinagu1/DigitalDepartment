using DigitalDepartment.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.DocumentCategories;
using Shared.DataTransferObjects.Position;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DigitalDepartment.Presentation.Controllers
{
    [Route("api/positions")]
    [ApiController]
    [Authorize]
    public class PositionController : ControllerBase
    {
        private readonly IServiceManager _service;
        public PositionController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPositions(
        [FromQuery] PositionParameters positionParameters)
        {
            var pagedResult = await
                _service.PositionService.GetAllPositionsAsync(positionParameters, false);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.positions);
        }


        [HttpGet("{id:int}", Name = "PositionById")]
        public async Task<IActionResult> GetPositionById(int id)
        {
            var dc = await
                _service.PositionService.GetPositionByIdAsync(id, false);
            return Ok(dc);
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreatePosition
            ([FromBody] PositionForCreationDto position)
        {
            var createdPosition =
                await _service.PositionService.CreatePositionAsync(position);
            return CreatedAtRoute("PositionById",
                new { id = createdPosition.Id },
                createdPosition);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdatePosition
            (int id,
            [FromBody] PositionForUpdateDto position)
        {
            await _service.PositionService.UpdatePosition
                (id, position, false);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePosition(int id)
        {
            await _service.PositionService.DeletePositionAsync(id, false);
            return NoContent();
        }
    }
}
