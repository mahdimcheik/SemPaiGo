using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SemPaiGo.Models;
using SemPaiGo.Utilities;

namespace SemPaiGo.Contexts;

public class MainContext : IdentityDbContext<UserApp, RoleApp, Guid>
{
    public DbSet<UserApp> Users { get; set; }
    public DbSet<RoleApp> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Gender> Genders { get; set; }

    // Profile entities
    public DbSet<ProfileTeacher> ProfileTeachers { get; set; }
    public DbSet<ProfileStudent> ProfileStudents { get; set; }

    // Related entities
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cursus> Cursuses { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Formation> Formations { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<CategoryCursus> CategoryCursuses { get; set; }
    public DbSet<LevelCursus> LevelCursuses { get; set; }

    // reservations
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Order> Orders { get; set; }

    // payments and transactions
    public DbSet<Payment> Payments { get; set; } // paiement d'une commande
    public DbSet<TeacherPayout> TeacherPayouts { get; set; } // une ligne par professeur (les comptes des profs)
    public DbSet<TeacherWalletTransaction> TeacherWalletTransactions { get; set; } // historique des transactions du portefeuille, acomptes, retraits, remboursements
    public DbSet<TypeTeacherTransaction> TypeTransactions { get; set; } // types de transactions (acompte, retrait, remboursement)
    public DbSet<StatusTransaction> StatusTransactions { get; set; } // statuts des transactions (en attente, complété, échoué)
    public DbSet<PaymentMethod> PaymentMethods { get; set; } // méthodes de paiement (carte bancaire, PayPal, etc.)

    public MainContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Les noms des tables dans la base de données
        builder.Entity<UserApp>().ToTable("Users");
        builder.Entity<RoleApp>().ToTable("Roles");
        builder.Entity<Gender>().ToTable("Genders");
        builder.Entity<RefreshToken>().ToTable("RefreshTokens");

        // Entities  properties

        builder.Entity<UserApp>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).IsRequired().HasMaxLength(64);
            e.Property(u => u.FirstName).IsRequired().HasMaxLength(64);
            e.Property(u => u.LastName).IsRequired().HasMaxLength(64);
            e.Property(e => e.DateOfBirth).IsRequired().HasColumnType("timestamp with time zone");
            e.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            e.Property(a => a.UpdatedAt).IsRequired().HasColumnType("timestamp with time zone");

            e.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // relations
            e.HasOne(u => u.Gender).WithMany().HasForeignKey(u => u.GenderId);
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

        // addresses
        builder.Entity<Address>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.City).IsRequired().HasMaxLength(150);
            entity.Property(a => a.Country).IsRequired().HasMaxLength(150);
            entity.Property(a => a.Street).IsRequired().HasMaxLength(250);
            entity.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            entity.Property(a => a.ZipCode).HasMaxLength(255);
            entity.Property(a => a.Longitude).HasMaxLength(50);
            entity.Property(a => a.Latitude).HasMaxLength(50);

            // Relation avec TeacherProfile (optionnelle)
            entity
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec StudentProfile (optionnelle)
            entity
                .HasOne(a => a.Student)
                .WithMany(s => s.Addresses)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contrainte : une adresse doit avoir EXACTEMENT un propriétaire (with quoted column names for PostgreSQL)
            entity.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_Address_OneOwnerOnly",
                    @"(""TeacherId"" IS NOT NULL AND ""StudentId"" IS NULL)
              OR
              (""TeacherId"" IS NULL AND ""StudentId"" IS NOT NULL)"
                )
            );
        });

        //  cursus
        builder.Entity<Cursus>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Description).HasMaxLength(255);
            entity.Property(a => a.ImgUrl).HasMaxLength(255);

            // Relation avec Teacher
            entity
                .HasOne(a => a.Teacher)
                .WithMany(t => t.Cursuses)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
            entity
                .HasOne(a => a.Level)
                .WithMany()
                .HasForeignKey(a => a.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasMany(a => a.Categories)
                .WithMany(c => c.Cursuses)
                .UsingEntity<Dictionary<string, object>>(
                    "CursusXCategories",
                    j =>
                        j.HasOne<CategoryCursus>()
                            .WithMany()
                            .HasForeignKey("CategorieId")
                            .OnDelete(DeleteBehavior.Restrict),
                    j =>
                        j.HasOne<Cursus>()
                            .WithMany()
                            .HasForeignKey("CursusId")
                            .OnDelete(DeleteBehavior.Restrict)
                );
        });

        // experiences
        builder.Entity<Experience>(e =>
        {
            e.HasKey(e => e.Id);
            e.Property(e => e.Title).IsRequired().HasMaxLength(200);
            e.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            e.Property(e => e.Company).IsRequired().HasMaxLength(200);
            e.Property(e => e.DateFrom).IsRequired().HasColumnType("timestamp with time zone");
            e.Property(e => e.DateTo).HasColumnType("timestamp with time zone");
            e.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            e.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            // relation avec Teacher
            e.HasOne(e => e.Teacher)
                .WithMany(t => t.Experiences)
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Formations
        builder.Entity<Formation>(f =>
        {
            f.HasKey(e => e.Id);
            f.Property(e => e.Title).IsRequired().HasMaxLength(200);
            f.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            f.Property(e => e.Institute).IsRequired().HasMaxLength(200);
            f.Property(e => e.DateFrom).IsRequired().HasColumnType("timestamp with time zone");
            f.Property(e => e.DateTo).HasColumnType("timestamp with time zone");
            f.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            f.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            f.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            // Relation avec TeacherProfile (optionnelle)
            f.HasOne(a => a.Teacher)
                .WithMany(t => t.Formations)
                .HasForeignKey(a => a.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relation avec StudentProfile (optionnelle)
            f.HasOne(a => a.Student)
                .WithMany(s => s.Formations)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Contrainte : une formation doit avoir EXACTEMENT un propriétaire (with quoted column names for PostgreSQL)
            f.ToTable(t =>
                t.HasCheckConstraint(
                    "CK_Formation_OneOwnerOnly",
                    @"(""TeacherId"" IS NOT NULL AND ""StudentId"" IS NULL)
              OR
              (""TeacherId"" IS NULL AND ""StudentId"" IS NOT NULL)"
                )
            );
        });

        //languages
        builder.Entity<Language>(l =>
        {
            l.HasKey(e => e.Id);
            l.Property(e => e.Name).IsRequired().HasMaxLength(200);
            l.Property(e => e.Color).IsRequired().HasMaxLength(200);
            l.Property(e => e.Icon).HasMaxLength(200);
            l.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            l.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            l.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
        });

        // ProfileTeacher
        builder.Entity<ProfileTeacher>(pt =>
        {
            pt.HasKey(e => e.Id);
            pt.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            pt.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            pt.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            pt.HasOne(s => s.User).WithOne().HasForeignKey<ProfileTeacher>(s => s.UserId);
        });

        // ProfileStudent
        builder.Entity<ProfileStudent>(ps =>
        {
            ps.HasKey(e => e.Id);
            ps.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            ps.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            ps.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            ps.HasOne(s => s.User).WithOne().HasForeignKey<ProfileStudent>(s => s.UserId);
        });

        // Slot
        builder.Entity<Slot>(s =>
        {
            s.HasKey(e => e.Id);
            s.Property(e => e.DateFrom).IsRequired().HasColumnType("timestamp with time zone");
            s.Property(e => e.DateTo).IsRequired().HasColumnType("timestamp with time zone");
            s.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            s.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            s.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            s.HasOne(s => s.Teacher)
                .WithMany(t => t.Slots)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            s.HasOne(s => s.Reservation)
                .WithOne(r => r.Slot)
                .HasForeignKey<Reservation>(r => r.SlotId);
        });
        // Reservation
        builder.Entity<Reservation>(r =>
        {
            r.HasKey(e => e.Id);
            r.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            r.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            r.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            r.HasOne(s => s.Student)
                .WithMany(t => t.Reservations)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //order
        builder.Entity<Order>(o =>
        {
            o.HasKey(e => e.Id);
            o.Property(e => e.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            o.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            o.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            o.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
            o.HasOne(o => o.Student)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Payment
        builder.Entity<Payment>(p =>
        {
            p.HasKey(e => e.Id);
            p.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
            p.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            p.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            p.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            p.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            p.HasOne(p => p.Method).WithMany().HasForeignKey(p => p.MethodId);
        });

        // paymentMethod
        builder.Entity<PaymentMethod>(p =>
        {
            p.HasKey(e => e.Id);
            p.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            p.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            p.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
        });

        // TeacherWalletTransaction
        builder.Entity<TeacherWalletTransaction>(p =>
        {
            p.HasKey(e => e.Id);
            p.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
            p.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            p.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            p.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");

            p.HasOne(p => p.Type).WithOne().HasForeignKey<TeacherWalletTransaction>(p => p.TypeId);
            p.HasOne(p => p.Reservation).WithMany().HasForeignKey(p => p.ReservationId);
        });

        // status transaction
        builder.Entity<StatusTransaction>(p =>
        {
            p.HasKey(e => e.Id);
            p.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            p.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            p.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
        });

        // type transaction
        builder.Entity<TypeTeacherTransaction>(p =>
        {
            p.HasKey(e => e.Id);
            p.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp with time zone")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            p.Property(e => e.UpdatedAt).HasColumnType("timestamp with time zone");
            p.Property(e => e.ArchivedAt).HasColumnType("timestamp with time zone");
        });

        // Seed Roles
        List<RoleApp> roles = new()
        {
            new RoleApp
            {
                Id = HardCode.ROLE_SUPER_ADMIN,
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN",
                DisplayName = "Super Administrateur",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ConcurrencyStamp = "SUPERADMIN-STAMP-2025",
            },
            new RoleApp
            {
                Id = HardCode.ROLE_ADMIN,
                Name = "Admin",
                NormalizedName = "ADMIN",
                DisplayName = "Administrateur",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ConcurrencyStamp = "SUPERADMIN-STAMP-2025",
            },
            new RoleApp
            {
                Id = HardCode.ROLE_TEACHER,
                Name = "Teacher",
                NormalizedName = "TEACHER",
                DisplayName = "Professeur",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ConcurrencyStamp = "SUPERADMIN-STAMP-2025",
            },
            new RoleApp
            {
                Id = HardCode.ROLE_STUDENT,
                Name = "Student",
                NormalizedName = "STUDENT",
                DisplayName = "Elève",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ConcurrencyStamp = "SUPERADMIN-STAMP-2025",
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
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Gender
            {
                Id = HardCode.GENDER_MALE,
                Name = "Male",
                Color = "#fa69b4",
                Icon = "",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
            new Gender
            {
                Id = HardCode.GENDER_OTHER,
                Name = "Other",
                Color = "#ab69b4",
                Icon = "",
                CreatedAt = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            },
        };

        builder.Entity<Gender>().HasData(genders);
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
