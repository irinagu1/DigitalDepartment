using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class DocumentParameters : RequestParameters
    {
        public DocumentParameters() => OrderBy = "CreationDate";

        //filter
        public string ForWho { get; set; } = "Общие";
        public string WhatType { get; set; } = "Для просмотра";

        public string? Category { get; set; }
        public string? Status { get; set; }
        public string CreationDate { get; set; }
        public bool? IsSigned {  get; set; }

        //search
        public string? SearchByName { get; set; }
        public string? SearchByAuthor { get; set; }


    }
}
