using Microsoft.EntityFrameworkCore;
using SRInfraInventorySystem.Core.Entities;
using System;

namespace SRInfraInventorySystem.Infrastructure.Persistence
{
    /// <summary>
    /// Uygulamanın ana veritabanı context'i.
    /// Entity Framework Core ile veritabanı işlemlerini yönetir.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// ApplicationDbContext'in yeni bir örneğini oluşturur.
        /// </summary>
        /// <param name="options">DbContext konfigürasyon seçenekleri</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Sunucular tablosu
        /// </summary>
        public DbSet<Server> Servers { get; set; }
        /// <summary>
        /// Uygulamalar tablosu
        /// </summary>
        public DbSet<Application> Applications { get; set; }
        /// <summary>
        /// Veritabanları tablosu
        /// </summary>
        public DbSet<Database> Databases { get; set; }
        /// <summary>
        /// Erişim logları tablosu
        /// </summary>
        public DbSet<AccessLog> AccessLogs { get; set; }
        /// <summary>
        /// Sunucu kullanım geçmişi tablosu
        /// </summary>
        public DbSet<ServerUsageHistory> ServerUsageHistories { get; set; }
        /// <summary>
        /// Departmanlar tablosu
        /// </summary>
        public DbSet<Department> Departments { get; set; }
        /// <summary>
        /// Personeller tablosu
        /// </summary>
        public DbSet<Personnel> Personnel { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Server Configuration
            modelBuilder.Entity<Server>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IpAddress).IsRequired().HasMaxLength(15);
                entity.Property(e => e.OperatingSystem).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CpuInfo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ResponsiblePerson).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Application Configuration
            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DnsName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Protocol).IsRequired().HasMaxLength(10);
                entity.Property(e => e.ResponsiblePerson).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Version).HasMaxLength(20);
                entity.HasOne(e => e.Server).WithMany(e => e.Applications).HasForeignKey(e => e.ServerId);
            });

            // Database Configuration
            modelBuilder.Entity<Database>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Version).HasMaxLength(20);
                entity.Property(e => e.ResponsiblePerson).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AccessPermissions).HasMaxLength(500);
                entity.Property(e => e.ConnectionTools).HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.HasOne(e => e.Server).WithMany(e => e.Databases).HasForeignKey(e => e.ServerId);
                entity.HasOne(e => e.ConnectedApplication).WithMany(e => e.ConnectedDatabases).HasForeignKey(e => e.ApplicationId);
            });

            // AccessLog Configuration
            modelBuilder.Entity<AccessLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.AccessMethod).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Permissions).HasMaxLength(200);
                entity.Property(e => e.IpAddress).HasMaxLength(15);
                entity.Property(e => e.UserAgent).HasMaxLength(500);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.HasOne(e => e.Server).WithMany().HasForeignKey(e => e.ServerId);
                entity.HasOne(e => e.Application).WithMany(e => e.AccessLogs).HasForeignKey(e => e.ApplicationId);
                entity.HasOne(e => e.Database).WithMany(e => e.AccessLogs).HasForeignKey(e => e.DatabaseId);
            });

            // ServerUsageHistory Configuration
            modelBuilder.Entity<ServerUsageHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.HasOne(e => e.Server).WithMany(e => e.UsageHistory).HasForeignKey(e => e.ServerId);
            });

            // Department Configuration
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                // Parent Department ilişkisi
                entity.HasOne(d => d.ParentDepartment)
                    .WithMany(p => p.SubDepartments)
                    .HasForeignKey(d => d.ParentDepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Manager Personnel ilişkisi
                entity.HasOne(d => d.ManagerPersonnel)
                    .WithMany()
                    .HasForeignKey(d => d.ManagerPersonnelId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Personnel Configuration
            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.IdentityNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Department).WithMany(e => e.Personnel).HasForeignKey(e => e.DepartmentId);
            });

            }
    }
}
