using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task.Core.Models;

namespace Task.Data;

public partial class WesIdentityContext : DbContext
{
    public WesIdentityContext()
    {
    }

    public WesIdentityContext(DbContextOptions<WesIdentityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }


    public virtual DbSet<Owner> Owners { get; set; }
    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.TenantId, "IX_AspNetRoles_TenantId");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AspNetRoles).HasForeignKey(d => d.TenantId);

            entity.HasMany(d => d.UsersNavigation).WithMany(p => p.RolesNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUserDictionaryStringObject",
                    r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UsersId"),
                    l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RolesId"),
                    j =>
                    {
                        j.HasKey("RolesId", "UsersId").HasName("PRIMARY");
                        j.ToTable("RoleUser (Dictionary<string, object>)");
                        j.HasIndex(new[] { "UsersId" }, "IX_RoleUser (Dictionary<string, object>)_UsersId");
                    });
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.OwnerId, "IX_AspNetUsers_OwnerId");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.LastLoginTime).HasMaxLength(6);
            entity.Property(e => e.LockoutEnabled)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.LockoutEnd).HasMaxLength(6);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.RegistrationDate).HasMaxLength(6);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Owner).WithMany(p => p.AspNetUsers).HasForeignKey(d => d.OwnerId);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PRIMARY");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name }).HasName("PRIMARY");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Owner");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Permission");

            entity.HasMany(d => d.Roles).WithMany(p => p.Permissions)
                .UsingEntity<Dictionary<string, object>>(
                    "PermissionRoleDictionaryStringObject",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RolesId")
                        .HasConstraintName("FK_PermissionRole (Dictionary<string, object>)_AspNetRoles_Role~"),
                    l => l.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionsId")
                        .HasConstraintName("FK_PermissionRole (Dictionary<string, object>)_Permission_Permi~"),
                    j =>
                    {
                        j.HasKey("PermissionsId", "RolesId").HasName("PRIMARY");
                        j.ToTable("PermissionRole (Dictionary<string, object>)");
                        j.HasIndex(new[] { "RolesId" }, "IX_PermissionRole (Dictionary<string, object>)_RolesId");
                    });
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Tenant");

            entity.HasIndex(e => e.OwnerId, "IX_Tenant_OwnerId");

            entity.HasOne(d => d.Owner).WithMany(p => p.Tenants).HasForeignKey(d => d.OwnerId);
        });

        modelBuilder.Entity<RoleUser>().HasKey(entity => new
        {
            entity.UsersId, entity.RolesId
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
