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
    internal class PermissionsRolesConfiguration : IEntityTypeConfiguration<PermissionRole>
    {
        public void Configure(EntityTypeBuilder<PermissionRole> builder)
        {
            builder.HasData(
                //admin
                new PermissionRole
                {
                    RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                    PermissionId = 1
                },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 2
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 3
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 4
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 5
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 6
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 7
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 8
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 9
                 },
                 new PermissionRole
                 {
                     RoleId = "9365b6ea-c516-4174-a231-43c5975bb099",
                     PermissionId = 10
                 },
                 new PermissionRole
                 {
                     RoleId = "483d51a8-37f5-473c-a17a-0b0d175c1e7e",
                     PermissionId = 11
                 }

            );
        }
    }
}
