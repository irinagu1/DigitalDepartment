using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class DocumentStatusConfiguration : IEntityTypeConfiguration<DocumentStatus>
    {
        public void Configure(EntityTypeBuilder<DocumentStatus> builder)
        {
            builder.HasData
            (
                new DocumentStatus
                {
                    Id = 1,
                    Name = "New",
                    isEnable = true
                },
                new DocumentStatus
                {
                    Id = 2,
                    Name = "In process",
                    isEnable = true
                },
                new DocumentStatus
                {
                    Id = 3,
                    Name = "Finished",
                    isEnable = true
                },
                new DocumentStatus
                {
                    Id = 4,
                    Name = "Closed",
                    isEnable = true
                }
            );
        }
    }
}
