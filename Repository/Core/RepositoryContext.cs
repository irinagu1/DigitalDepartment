using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Repository.Core
{
    public class RepositoryContext : IdentityDbContext<User, RoleEntity, string>
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DocumentCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            modelBuilder.Entity<PermissionRoleEntity>()
                .HasKey(pr => new { pr.RoleEntityId, pr.PermissionId });
            modelBuilder.Entity<PermissionRoleEntity>()
                .HasOne(pr => pr.RoleEntity)
                .WithMany(r => r.PermissionRoleEntities)
                .HasForeignKey(pr => pr.RoleEntityId);

            modelBuilder.Entity<PermissionRoleEntity>()
                .HasOne(pr => pr.Permission)
                .WithMany(p => p.PermissionRoleEntities)
                .HasForeignKey(pr => pr.PermissionId);

            modelBuilder.ApplyConfiguration(new PermissionsRolesConfiguration());
        }

        public DbSet<DocumentCategory>? DocumentCategories { get; set; }
        public DbSet<DocumentStatus>? DocumentStatuses { get; set; }
        public DbSet<Document>? Documents { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<RoleEntity> Roles {  get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRoleEntity> PermissionRoleEntities {  get; set; }
    }
}
