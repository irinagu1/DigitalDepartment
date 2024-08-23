﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.DocumentStatuses
{
    public record DocumentStatusDto
    (
        int Id, 
        string Name,
        bool isEnable
    );
}