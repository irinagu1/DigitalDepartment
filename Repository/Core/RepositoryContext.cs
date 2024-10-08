using Entities.Models;
using Entities.Models.Auth;
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
    public class RepositoryContext : IdentityDbContext
        <
            User, 
            Role, 
            string, 
            IdentityUserClaim<string>, 
            UserRole, 
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>, 
            IdentityUserToken<string>
        >
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(u =>
                u.HasMany(e => e.UserRoles)
                .WithOne(e => e.User)
                .HasForeignKey(ut => ut.UserId)
                .IsRequired()
                );

            modelBuilder.Entity<Role>(b =>
                b.HasMany(e => e.UserRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                );

            modelBuilder.Entity<Role>(b =>
                b.HasMany(e => e.PermissionRoles)
                .WithOne(e => e.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired()
                );

            modelBuilder.Entity<Permission>(b =>
               b.HasMany(e => e.PermissionRoles)
               .WithOne(e => e.Permission)
               .HasForeignKey(ur => ur.PermissionId)
               .IsRequired()
               );

            modelBuilder.Entity<PermissionRole>()
                .HasKey(t => new { 
                                    t.RoleId, 
                                    t.PermissionId 
                });

            modelBuilder.ApplyConfiguration(new DocumentCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionsRolesConfiguration());
        }

        public DbSet<DocumentCategory>? DocumentCategories { get; set; }
        public DbSet<DocumentStatus>? DocumentStatuses { get; set; }
        public DbSet<Document>? Documents { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<ToCheck> ToChecks  { get; set; }
        public DbSet<Role> Roles {  get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles {  get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        
        
    }
}
