﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Letters
{
    public class LetterDto
    {
        public string Id { get; set; }
        
        public string? Text {  get; set; }
        public string? AuthorId { get; set; }

        public DateTime? CreationDate { get; set; }

    }
}
