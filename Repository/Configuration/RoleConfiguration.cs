using Entities.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.HasData(
                 new Role
                 {
                     Id = "483d51a8-37f5-473c-a17a-0b0d175c1e7e",
                     Name = "Manager",
                     NormalizedName = "MANAGER"
                 },
                 new Role
                 {
                     Id = "9365b6ea-c516-4174-a231-43c5975bb099",
                     Name = "Administrator",
                     NormalizedName = "ADMINISTRATOR"
                 }
            );
        }
    }
}
