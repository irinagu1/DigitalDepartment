using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public record DocumentForCreationDto : DocumentForManipulationDto
    {
        [Required(ErrorMessage = "Please add a file")]
        public IFormFile? File { get; init; }

    }
}
