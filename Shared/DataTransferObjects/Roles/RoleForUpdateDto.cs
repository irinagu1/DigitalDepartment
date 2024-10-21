﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Roles
{
    public class RoleForUpdateDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public bool IsActived { get; set; }
    }
}