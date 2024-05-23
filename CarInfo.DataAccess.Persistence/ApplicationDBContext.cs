using CarInfo.DataAccess.Domain;
using CarInfo.DataAccess.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.DataAccess.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Car> car { get; set; }
        public DbSet<Manufacturer> manufacturer { get; set; }
        public DbSet<CarType> carType { get; set; }
        public DbSet<CarTransmissionType> carTransmissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Car>()
                .HasOne<Manufacturer>()
                .WithMany()
                .HasForeignKey(x => x.manifactureId);

            modelBuilder.Entity<Car>()
               .HasOne<CarType>()
               .WithMany()
               .HasForeignKey(x => x.typeId);

            modelBuilder.Entity<Car>()
               .HasOne<CarTransmissionType>()
               .WithMany()
               .HasForeignKey(x => x.carTransmissionId);

            modelBuilder.Entity<Car>()
               .HasIndex(u => u.model)
               .IsUnique();
            modelBuilder.Entity<Manufacturer>()
               .HasIndex(u => u.name)
               .IsUnique();
            modelBuilder.Entity<Manufacturer>()
               .HasIndex(u => u.contactPerson)
               .IsUnique();

        }
    }

}
