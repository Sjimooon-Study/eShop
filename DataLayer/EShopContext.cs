using DataLayer.Models;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class EShopContext : DbContext
    {
        public EShopContext() { }
        public EShopContext(DbContextOptions<EShopContext> options) : base (options) { }
        
        public DbSet<DigitalDecoder> DigitalDecoders { get; set; }
        public DbSet<Locomotive> Locomotives { get; set; }
        public DbSet<RailCar> RailCars { get; set; }

        public DbSet<SiteUser> SiteUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                    .EnableSensitiveDataLogging(true)
                    .UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = EShopDb; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Data Seeding
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = "Sale" },
                new Tag { TagId = "New" }
                );

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, Name = "Denmark" },
                new Country { CountryId = 2, Name = "Germany" },
                new Country { CountryId = 3, Name = "Switzerland" }
                );

            modelBuilder.Entity<RailwayCompany>().HasData(
                new RailwayCompany { RailwayCompanyId = 1, Name = "DSB", CountryId = 1},
                new RailwayCompany { RailwayCompanyId = 2, Name = "DB", CountryId = 2},
                new RailwayCompany { RailwayCompanyId = 3, Name = "DR", CountryId = 2},
                new RailwayCompany { RailwayCompanyId = 4, Name = "SBB", CountryId = 3}
                );

            modelBuilder.Entity<Image>().HasData(
                // PluX22 sound decoder
                //new Image { ImageId = 1, Path = "" },
                // Re 460
                new Image { ImageId = 1, Path = "2021_10_20_b51acd9b-05bc-415d-86ac-c3c2e17b0012.jpeg" },
                // BR 023
                new Image { ImageId = 2, Path = "2021_10_20_c74b3599-6852-459a-9ee7-1b9129dcf3e0.jpeg" },
                // BR 52
                new Image { ImageId = 3, Path = "2021_10_20_06e1d3fa-8751-4c0e-9fac-b830384b0051.jpeg" },
                new Image { ImageId = 4, Path = "2021_10_20_8aa35a49-50cd-4950-8689-38d547c35c91.jpeg" },
                new Image { ImageId = 5, Path = "2021_10_20_a6c0169e-1d21-47ce-acef-b0ac88de5990.jpeg" },
                new Image { ImageId = 6, Path = "2021_10_20_de2a7fc8-702f-4965-893e-816a22a4f403.jpeg" },
                new Image { ImageId = 7, Path = "2021_10_20_eb718ad5-202f-4fe7-9f36-772da6d3b9a4.jpeg" },
                new Image { ImageId = 8, Path = "2021_10_20_fa6c395b-d19f-44bc-9afc-d61d0905fe81.jpeg" }
                );

            modelBuilder.Entity<DigitalDecoder>().HasData(
                new DigitalDecoder
                {
                    ProductId = 1,
                    Name = "PluX22 sound decoder (NEM 658)",
                    Description = "Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.",
                    AmountInStock = 23,
                    Price = 92.4M,
                    TagId = null,

                    Interface = DigitalDecoder.EDecoderInterface.PluX22,
                    Sound = true
                }
                );

            modelBuilder.Entity<Locomotive>().HasData(
                new Locomotive
                {
                    ProductId = 2,
                    Name = "BR 023 040-9",
                    Description = "The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.",
                    Price = 229.9M,
                    AmountInStock = 5,
                    TagId = "New",

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.IV,

                    Length = 24.5f,
                    NumOfAxels = 9,
                    RailwayCompanyId = 2,

                    Control = Locomotive.EControl.DC,
                    LocoType = Locomotive.ELocoType.SteamLocomotive,
                    AutoCoupling = false,
                    NumOfDrivenAxels = 4,
                    DigitalDecoderId = null
                },
                new Locomotive
                {
                    ProductId = 3,
                    Name = "Re 460 068-0",
                    Description = "In 1992, the first locomotive Re 460 of the Swiss Federal Railways rolled out of the factory halls of the companies SLM and BBC in Oerlikon, Switzerland. The locomotive became known to the public as \"Lok 2000\". It stands for fast and modern passenger transport in Switzerland. An eye-catching and particularly aerodynamic design with a large front window, roof cladding and beads on the side wall make the class 460 visually an unbeatable rail vehicle.",
                    Price = 321.9M,
                    AmountInStock = 2,
                    TagId = null,

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.VI,

                    Length = 21.2f,
                    NumOfAxels = 4,
                    RailwayCompanyId = 4,

                    Control = Locomotive.EControl.DCC,
                    LocoType = Locomotive.ELocoType.ElectricLocomotive,
                    AutoCoupling = false,
                    NumOfDrivenAxels = 4,
                    DigitalDecoderId = 1
                },
                new Locomotive
                {
                    ProductId = 4,
                    Name = "BR 52",
                    Description = "In the period between 1942 to 1950, over 7000 units of the class 52 war locomotive were built. These were constructed with as little effort as possible and savings were also made on the material wherever possible. With a weight of 84 tons, the loco achieved an output of 1,192 kW and a top speed of 80 km / h. The Deutsche Bundesbahn mainly got rid of the locomotives as early as 1953 - since it had sufficient machines of the series 50 and series 44 to haul the heavy goods trains. Only a few locomotives built in 1945 remained with the DB until 1962.",
                    Price = 319.9M,
                    AmountInStock = 1,
                    TagId = "New",

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.III,

                    Length = 26.5f,
                    NumOfAxels = 10,
                    RailwayCompanyId = 2,

                    Control = Locomotive.EControl.DC,
                    LocoType = Locomotive.ELocoType.SteamLocomotive,
                    AutoCoupling = false,
                    NumOfDrivenAxels = 4,
                    DigitalDecoderId = null
                }
                );

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithMany(i => i.Products)
                .UsingEntity(j => j.HasData(
                    new { ProductsProductId = 2, ImagesImageId = 2},
                    new { ProductsProductId = 3, ImagesImageId = 1},
                    new { ProductsProductId = 4, ImagesImageId = 3},
                    new { ProductsProductId = 4, ImagesImageId = 4},
                    new { ProductsProductId = 4, ImagesImageId = 5},
                    new { ProductsProductId = 4, ImagesImageId = 6},
                    new { ProductsProductId = 4, ImagesImageId = 7}
                    ));

            modelBuilder.Entity<SiteUser>().HasData(
                new SiteUser
                {
                    SiteUserId = 1,
                    UserName = "admin",
                    Password = "admin",
                    IsAdmin = true
                }
                );
            #endregion
        }
    }
}
