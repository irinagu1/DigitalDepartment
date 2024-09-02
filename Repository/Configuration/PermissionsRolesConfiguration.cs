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
    internal class PermissionsRolesConfiguration : IEntityTypeConfiguration<PermissionRoleEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionRoleEntity> builder)
        {
            builder.HasData(
                //admin
                 new PermissionRoleEntity
                 {
                    RoleEntityId = "483d51a8-37f5-473c-a17a-0b0d175c1e7e",
                    PermissionId =1
                 },
                 new PermissionRoleEntity
                 {
                     RoleEntityId = "483d51a8-37f5-473c-a17a-0b0d175c1e7e",
                     PermissionId = 2
                 },
                  //manager
                  new PermissionRoleEntity
                  {
                      RoleEntityId = "9365b6ea-c516-4174-a231-43c5975bb099",
                      PermissionId = 1
                  },
                   new PermissionRoleEntity
                   {
                       RoleEntityId = "9365b6ea-c516-4174-a231-43c5975bb099",
                       PermissionId = 2
                   },
                   new PermissionRoleEntity
                   {
                       RoleEntityId = "9365b6ea-c516-4174-a231-43c5975bb099",
                       PermissionId = 3
                   }
            );
        }
    }
}
