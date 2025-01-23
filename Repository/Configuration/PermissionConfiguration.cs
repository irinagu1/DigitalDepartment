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
                      Name = "Просмотр справочников",
                      Category = "Справочные данные",
                  },
                  new Permission
                  {
                      Id = 2,
                      Name = "Редактирование справочников",
                      Category = "Справочные данные",
                  },
                  new Permission
                  {
                      Id = 3,
                      Name = "Управление ролями",
                      Category = "Роли",
                  },
                  new Permission
                  {
                      Id = 4,
                      Name = "Просмотр пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 5,
                      Name = "Редактирование пользователей",
                      Category = "Пользователи",
                  },
                  new Permission
                  {
                      Id = 6,
                      Name = "Просмотр информации о себе",
                      Category = "О себе",
                  },
                  new Permission
                  {
                      Id = 7,
                      Name = "Просмотр всех документов",
                      Category = "Документы и архив",
                  },
                  new Permission
                  {
                      Id = 8,
                      Name = "Редактирование всех документов",
                      Category = "Документы",
                  },
                  new Permission
                  {
                      Id = 9,
                      Name = "Добавление документов",
                      Category = "Общие возможности",
                  },
                   new Permission
                   {
                       Id = 10,
                       Name = "Просмотр отправленных пользователю",
                       Category = "Общие возможности",
                   },
                   new Permission
                   {
                       Id = 11,
                       Name = "Просмотр созданных пользователем",
                       Category = "Общие возможности",
                   },
                    new Permission
                    {
                        Id = 12,
                        Name = "Редактирование шаблона отчета",
                        Category = "Шаблон отчета",
                    }

            );
        }
    }
}
