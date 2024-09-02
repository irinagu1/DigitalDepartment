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
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                 new Permission
                 {
                     Id = 1,
                     Name = "Create",
                 },
                 new Permission
                 {
                     Id = 2,
                     Name = "Read",
                 },
                 new Permission
                 {
                     Id = 3,
                     Name = "Update",
                 },
                 new Permission
                 {
                     Id = 4,
                     Name = "Delete",
                 }
            );
        }
    }
}
