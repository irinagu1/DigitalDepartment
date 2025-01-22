using Entities.Models;
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
    internal class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {

            builder.HasData(
                 new Position
                 {
                     Id = 1,
                     Name = "FullAccessAdmin",
                     isEnable = true
                 }
            );
        }
    }
}
