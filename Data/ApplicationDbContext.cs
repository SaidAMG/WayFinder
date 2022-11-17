using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WayFinder.Models;

namespace WayFinder.Data
{
    public class ApplicationDbContext : IdentityDbContext<Resident>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Resident> Residents { get; set; }

        public virtual DbSet<LocationSearchLog> LocationSearchLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var level = new Level
            {
                ID = 1,
                LevelNumber = 0
            };

            var location = new Location
            {
                ID = 1,
                Name = "Ruimte 001",
                Description = "Kantoor",
                X = 1,
                Y = 1,
                LevelID = 1
            };

            var location2 = new Location
            {
                ID = 2,
                Name = "Ruimte 010",
                Description = "Kantoor",
                X = 5,
                Y = 9,
                LevelID = 1
            };

            var company = new Company
            {
                ID = 1,
                Name = "Brightlands",
                LocationID = 1
            };

            var company2 = new Company
            {
                ID = 2,
                Name = "Data4",
                LocationID = 2
            };

            var resident = new Resident
            {
                FirstName = "Piet",
                LastName = "Jan",
                UserName = "PietJan",
                Email = "pietjan@gmail.com",
                CompanyID = 1,
                EmailConfirmed = true,
                NormalizedUserName = "PIETJAN",
                NormalizedEmail = "PIETJAN@GMAIL.COM"
            };

            var resident2 = new Resident
            {
                FirstName = "Jan",
                LastName = "Pillenman",
                UserName = "JanPillenman",
                Email = "jan.pillenman@gmail.com",
                CompanyID = 2,
                EmailConfirmed = true,
                NormalizedUserName = "JANPILLENMAN",
                NormalizedEmail = "JAN.PILLENMAN@GMAIL.COM"
            };

            var passwordHasher = new PasswordHasher<Resident>().HashPassword(resident, "PietJan123!");
            resident.PasswordHash = passwordHasher;

            var passwordHasher2 = new PasswordHasher<Resident>().HashPassword(resident2, "JanPillenman123!");
            resident2.PasswordHash = passwordHasher2;

            builder.Entity<Level>().HasData(level);
            builder.Entity<Location>().HasData(location);
            builder.Entity<Location>().HasData(location2);
            builder.Entity<Company>().HasData(company);
            builder.Entity<Company>().HasData(company2);
            builder.Entity<Resident>().HasData(resident);
            builder.Entity<Resident>().HasData(resident2);

            builder.Entity<Location>()
                .HasOne(c => c.Level)
                .WithMany(c => c.Locations)
                .HasForeignKey(c => c.LevelID);

            builder.Entity<Company>()
                .HasOne(c => c.Location)
                .WithMany(b => b.Company)
                .HasForeignKey(c => c.LocationID);

            builder.Entity<Resident>()
                .HasOne(c => c.Company)
                .WithMany(b => b.Residents);

            builder.Entity<Company>()
                .HasMany(c => c.Events)
                .WithMany(c => c.Companies);

            builder.Entity<Guest>()
                .HasMany(c => c.Events)
                .WithMany(c => c.Guests);
        }

    }
}
