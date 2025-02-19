﻿using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ToCheck
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User? User { get; set; }

        
        [ForeignKey(nameof(Version))]
        public long? VersionId { get; set; }
        public DocumentVersion? Version { get; set; }

        public DateTime DateChecked { get; set; }
    }
}
