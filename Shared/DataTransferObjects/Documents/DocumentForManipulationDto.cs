using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public abstract record DocumentForManipulationDto
    {
        [Required(ErrorMessage = "Document name is a required field.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters.")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Document status is a required field.")]
        public int? DocumentStatusId { get; init; }
   
        [Required(ErrorMessage = "Document status is a required field.")]
        public int? DocumentCategoryId { get; init; }
        [Required(ErrorMessage = "Letter id is a required field.")]
        public int? LetterId { get; init;  }
        public bool isArchived { get; init; } = false;
    }
}
