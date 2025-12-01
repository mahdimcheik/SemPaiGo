using Hangfire.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SemPaiGo.Models;
using SemPaiGo.Utilities;
using System.Net;
using System.Reflection.Metadata;

namespace SemPaiGo.Contexts;

public class MainContext : IdentityDbContext<UserApp, RoleApp, Guid>
{
    public DbSet<UserApp> Users { get; set; }
    public DbSet<RoleApp> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Gender> Genders { get; set; }

    public MainContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Les noms des tables dans la base de données
        builder.Entity<UserApp>().ToTable("Users");
        builder.Entity<RoleApp>().ToTable("Roles");
        builder.Entity<Gender>().ToTable("Genders");

        // Entities  properties

        builder.Entity<UserApp>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).IsRequired().HasMaxLength(64);
            e.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            e.Property(u => u.LastName).IsRequired().HasMaxLength(64);
            e.Property(e => e.DateOfBirth)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
            e.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            e.Property(a => a.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone");

            e.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });        

        builder.Entity<RoleApp>(r =>
        {
            r.HasKey(r => r.Id);
            r.Property(r => r.Id).IsRequired().HasMaxLength(64);
            r.Property(r => r.Name).IsRequired().HasMaxLength(64);
            r.Property(r => r.NormalizedName).IsRequired().HasMaxLength(64);
            r.Property(r => r.DisplayName).IsRequired().HasMaxLength(128);
            r.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            r.Property(a => a.UpdatedAt).HasColumnType("timestamp with time zone");

            r.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        builder.Entity<RefreshToken>(r =>
        {
            r.HasKey(r => r.Id);
            r.Property(r => r.Id).IsRequired().HasMaxLength(64);
            r.Property(r => r.Token).IsRequired().HasMaxLength(256);
            r.Property(r => r.ExpirationDate)
                .IsRequired()
                .HasColumnType("timestamp with time zone");
        });

        builder.Entity<Gender>(g =>
        {
            g.HasKey(g => g.Id);
            g.Property(g => g.Id).IsRequired().HasMaxLength(64);
            g.Property(g => g.Name).IsRequired().HasMaxLength(64);
            g.Property(g => g.Color).IsRequired().HasMaxLength(16);
            g.Property(g => g.Icon).HasMaxLength(256);
            g.Property(g => g.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            g.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            g.Property(a => a.UpdatedAt).HasColumnType("timestamp with time zone");
        });   

        // relationships      
        builder
            .Entity<UserApp>()
            .HasOne(u => u.Gender)
            .WithMany()
            .HasForeignKey(u => u.GenderId);             
      

        // UserRole => UserApp, RoleApp
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>>(userRole =>
        {
            userRole
                .HasOne<RoleApp>()
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            userRole
                .HasOne<UserApp>()
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        });

        // User => RefreshToken
        builder
            .Entity<RefreshToken>()
            .HasOne(r => r.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(a => a.UserId);

      
        // Seed Roles
        List<RoleApp> roles = new()
            {
                new RoleApp
                {
                    Id = HardCode.ROLE_SUPER_ADMIN,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    DisplayName = "Super Administrateur",
                    CreatedAt = DateTime.UtcNow,
                },
                new RoleApp
                {
                    Id = HardCode.ROLE_ADMIN,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    DisplayName = "Administrateur",

                    CreatedAt = DateTime.UtcNow,
                },
                new RoleApp
                {
                    Id = HardCode.ROLE_TEACHER,
                    Name = "Teacher",
                    NormalizedName = "TEACHER",
                    DisplayName = "Professeur",
                    CreatedAt = DateTime.UtcNow,
                },
                new RoleApp
                {
                    Id = HardCode.ROLE_STUDENT,
                    Name = "Student",
                    NormalizedName = "STUDENT",
                    DisplayName = "Elève",
                    CreatedAt = DateTime.UtcNow,
                },
            };
        builder.Entity<RoleApp>().HasData(roles);
        // Seed Genders
        List<Gender> genders = new()
            {
                new Gender
                {
                    Id = HardCode.GENDER_FEMALE,
                    Name = "Female",
                    Color = "#ff69b4",
                    Icon = "",
                    CreatedAt = DateTime.UtcNow,
                },
                new Gender
                {
                    Id = HardCode.GENDER_MALE,
                    Name = "Male",
                    Color = "#fa69b4",
                    Icon = "",
                    CreatedAt = DateTime.UtcNow,
                },
                new Gender
                {
                    Id = HardCode.GENDER_OTHER,
                    Name = "Other",
                    Color = "#ab69b4",
                    Icon = "",
                    CreatedAt = DateTime.UtcNow,
                },
            };

        builder.Entity<Gender>().HasData(genders);

        // Global Query Filters to exclude soft-deleted entities
        //builder.Entity<Booking>().HasQueryFilter(b => b.ArchivedAt != null);
        //builder.Entity<UserApp>().HasQueryFilter(b => b.ArchivedAt != null);
        //builder.Entity<Slot>().HasQueryFilter(b => b.ArchivedAt != null);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<CustomDateTimeConversion>();
        base.ConfigureConventions(configurationBuilder);
    }
}
