using Entities.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    internal class UsersRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(
                new UserRole { 
                    UserId= "9f8f4248-953c-409b-a048-ac08324f19fe",
                    RoleId= "9365b6ea-c516-4174-a231-43c5975bb099"
                }

            );
        }
    }
}
