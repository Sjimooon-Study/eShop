using DataLayer.Models;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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
                new Image { ImageId = 1, Url = "https://www.roco.cc/doc/idimages/def2/1633611600/123106022013017006010001016009105021031014117.jpg" },
                // Re 460
                new Image { ImageId = 2, Url = "https://www.roco.cc/doc/idimages/def2/1633611600/123109024010020014010001016009105021031014117.jpg" },
                // BR 023
                new Image { ImageId = 3, Url = "https://www.roco.cc/doc/idimages/def2/1633611600/123109026011022009010001016009105021031014117.jpg" },
                // BR 52
                new Image { ImageId = 4, Url = "https://www.roco.cc/doc/idimages/def2/1633611600/123109023008017012010001016009105021031014117.jpg" },
                new Image { ImageId = 5, Url = "https://www.roco.cc/doc/idimages/def2/1633615200/126105029012017019089002030013103027028015121010010.jpg" }
                );

            modelBuilder.Entity<DigitalDecoder>().HasData(
                new DigitalDecoder
                {
                    ProductId = 1,
                    Name = "PluX22 sound decoder (NEM 658)",
                    Description = "Suitable for Gauge H0. The decoder is equipped with the RailCom® function. Maximum motor current: 1.2 A.",
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
                    TagId = "New",

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.IV,

                    Length = 24.5f,
                    NumOfAxels = 9,
                    RailWayCompanyId = 2,

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
                    TagId = null,

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.VI,

                    Length = 21.2f,
                    NumOfAxels = 4,
                    RailWayCompanyId = 4,

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
                    TagId = "New",

                    Scale = ModelItem.EScale.HO,
                    Epoch = ModelItem.EEpoch.III,

                    Length = 26.5f,
                    NumOfAxels = 10,
                    RailWayCompanyId = 2,

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
                    new { ProductsProductId = 1, ImagesImageId = 1},
                    new { ProductsProductId = 2, ImagesImageId = 3},
                    new { ProductsProductId = 3, ImagesImageId = 2},
                    new { ProductsProductId = 4, ImagesImageId = 4},
                    new { ProductsProductId = 4, ImagesImageId = 5}
                    ));
            
            modelBuilder.Entity<StockStatus>().HasData(
                new StockStatus
                {
                    StockStatusId = 1,
                    Amount = 23,
                    NextStock = DateTime.Now.AddMonths(1),
                    ProductId = 1
                },
                new StockStatus
                {
                    StockStatusId = 2,
                    Amount = 5,
                    NextStock = DateTime.Now.AddMonths(2),
                    ProductId = 2
                },
                new StockStatus
                {
                    StockStatusId = 3,
                    Amount = 2,
                    NextStock = DateTime.Now.AddDays(16),
                    ProductId = 3
                },
                new StockStatus
                {
                    StockStatusId = 4,
                    Amount = 1,
                    NextStock = DateTime.Now.AddMonths(7),
                    ProductId = 4
                }
                );
            #endregion
        }
    }
}
