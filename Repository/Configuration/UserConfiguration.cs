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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        //LOGIN: superadmin
        //PASSWORD: 123456789ii
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User() {
                    Id = "9f8f4248-953c-409b-a048-ac08324f19fe",
                    FirstName = "Full",
                    SecondName = "Access",
                    LastName = "Administrator",
                    PhoneNumber = "12345",
                    isActive = true,
                    PositionId = 1,
                    PasswordHash = "AQAAAAIAAYagAAAAECaFM/EyVqmzR0nPT9SFF6qvDJkp2rURb83BDmBcrM/Lb0ya3JZtbNxZVOpflyWy0w==",
                    UserName = "superadmin",
                    NormalizedUserName = "SUPERADMIN",
                    Email = "superadming@gmail.com",
                    NormalizedEmail = "SUPERADMIN@GMAIL.COM",
                }
            );
        }
    }
}
