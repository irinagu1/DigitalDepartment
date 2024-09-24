using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class DocumentParameters : RequestParameters
    {
        public string ForWho { get; set; } = "Общие";
        public string WhatType { get; set; } = "Для просмотра";
    }
}
