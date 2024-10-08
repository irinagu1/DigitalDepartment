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
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                  
                  new Permission
                  {
                      Id = 1,
                      Name = "Просмотр пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 2,
                      Name = "Добавление пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 3,
                      Name = "Редактирование пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 4,
                      Name = "Архивирование пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 5,
                      Name = "Удаление пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 6,
                      Name = "Просмотр своих документов",
                      Category = "Документы",
                  },
                  new Permission
                  {
                      Id = 7,
                      Name = "Загрузка документов",
                      Category = "Документы",
                  },
                  new Permission
                  {
                      Id = 8,
                      Name = "Просмотр архива документов",
                      Category = "Документы",
                  },
                  new Permission
                  {
                      Id = 9,
                      Name = "Архивирование документов",
                      Category = "Документы",
                  },
                   new Permission
                   {
                       Id = 10,
                       Name = "Просмотр всех документов",
                       Category = "Документы",
                   }

            );
        }
    }
}
