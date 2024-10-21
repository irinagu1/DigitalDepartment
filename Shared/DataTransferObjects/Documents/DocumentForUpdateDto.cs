﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Documents
{
    public class DocumentForUpdateDto
    {
        public int Id { get; init; }
        public string? Name { get; set; }
        public string? Path { get; set; }

        public int? DocumentStatusId { get; set; }

        public int? DocumentCategoryId { get; set; }
        public int? LetterId { get; set; }

        public bool? isArchived { get; set; }
    }
}