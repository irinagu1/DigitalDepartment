using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Configuration
{
    public class DocumentCategoryConfiguration : 
        IEntityTypeConfiguration<DocumentCategory>
    {
        public void Configure(EntityTypeBuilder<DocumentCategory> builder)
        {
            builder.HasData
            (
                new DocumentCategory
                {
                    Id = 1,
                    Name = "Schedule",
                    isEnable = true
                },
                new DocumentCategory
                {
                    Id = 2,
                    Name = "Report",
                    isEnable = true
                },
                 new DocumentCategory
                 {
                     Id = 3,
                     Name = "ThirdCategory",
                     isEnable = true
                 }
            );
        }
    }
}
