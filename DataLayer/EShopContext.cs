using DataLayer.Models;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class EShopContext : DbContext
    {
        public DbSet<DigitalDecoder> DigitalDecoders { get; set; }
        public DbSet<Locomotive> Locomotives { get; set; }
        public DbSet<RailCar> RailCars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = EShopDb; Trusted_Connection = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Data Seeding

            #endregion
        }
    }
}
