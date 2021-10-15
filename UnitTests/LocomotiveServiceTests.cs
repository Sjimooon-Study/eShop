using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Products;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using ServiceLayer.LocomotiveService;
using ServiceLayer.LocomotiveService.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class LocomotiveServiceTests
    {
        [Fact]
        public void AddSingleLocomotive()
        {
            // Arange
            AddLocomotiveDto locomotive = new AddLocomotiveDto
            {
                Name = "BR 023 040-9",
                Description = "The 023 series was a true all-round genius. The locomotive hauled commuter trains, fast and express trains. Sometimes they hauled even freight trains. The newly designed locomotive of the class 023 (which until 1968 was designated class 23) was being used even in the epoch IV. On Dec. 31 1971, 76 locomotives were a permanent part of the rolling stock of the DB and without exception they were stationed at the three railway depots Saarbrücken, Kaiserslautern and Crailsheim.",
                Price = 229.9M,
                ReusedImages = new List<int>(),
                AddedImages = new List<AddImageDto>(),
                Tag = "New",
                StockStatus = new AddStockStatusDto
                {
                    Amount = 1,
                    NextStock = DateTime.Now
                },

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
            };

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "AddSingleLocomotive")
                .Options;
            
            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                service.Add(locomotive);
            }

            // Assert
            using (var context = new EShopContext(options))
            {
                var result = context.Locomotives
                    .Select(l => new
                    {
                        Name = l.Name
                    }).FirstOrDefault();

                Assert.True(result != null);
                Assert.Equal(locomotive.Name, result.Name);
            }
        }

        [Fact]
        public void AddMultipleLocomotives()
        {
            // Arange
            var locomotives = new List<AddLocomotiveDto>()
            {
                new AddLocomotiveDto { Name = "Loco 1" },
                new AddLocomotiveDto { Name = "Loco 2" },
                new AddLocomotiveDto { Name = "Loco 3" }
            };
            List<string> result;
            
            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "AddMultipleLocomotives")
                .Options;

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                service.Add(locomotives);
            }

            // Assert
            using (var context = new EShopContext(options))
            {
                result = context.Locomotives
                    .Select(l => l.Name)
                    .ToList();

                Assert.True(result != null);
                Assert.Equal(locomotives.Count(), result.Count());
                Assert.Equal(result[0], locomotives[0].Name);
                Assert.Equal(result[1], locomotives[1].Name);
                Assert.Equal(result[2], locomotives[2].Name);
            }
        }

        [Fact]
        public void OrderLocomotivesByName()
        {
            // Arange
            var locomotives = new List<AddLocomotiveDto>()
            {
                new AddLocomotiveDto { Name = "B" },
                new AddLocomotiveDto { Name = "C" },
                new AddLocomotiveDto { Name = "A" }
            };
            var result = new List<ListLocomotiveDto>();

            var queryOptions = new QueryOptions
            {
                OrderByOptions = EOrderByOptions.ByNameAsc
            };

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "OrderLocomotivesByName")
                .Options;

            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                service.Add(locomotives);
            }

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);

                var query = service.GetListLocomotives(queryOptions);
                
                foreach (var locomotive in query.Item1)
                {
                    result.Add(locomotive);
                }
            }

            // Assert
            Assert.True(result != null);
            Assert.Equal(locomotives.Count(), result.Count());
            Assert.Equal(locomotives[2].Name, result[0].Name);
            Assert.Equal(locomotives[0].Name, result[1].Name);
            Assert.Equal(locomotives[1].Name, result[2].Name);
        }

        [Fact]
        public void OrderLocomotivesByPrice()
        {
            // Arange
            var locomotives = new List<AddLocomotiveDto>()
            {
                new AddLocomotiveDto { Price = 219.8M },
                new AddLocomotiveDto { Price = 89.0M },
                new AddLocomotiveDto { Price = 123.8M }
            };
            var result = new List<ListLocomotiveDto>();

            var queryOptions = new QueryOptions
            {
                OrderByOptions = EOrderByOptions.ByPriceDesc
            };

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "OrderLocomotivesByPrice")
                .Options;

            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                service.Add(locomotives);
            }

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);

                var query = service.GetListLocomotives(queryOptions);

                foreach (var locomotive in query.Item1)
                {
                    result.Add(locomotive);
                }
            }

            // Assert
            Assert.True(result != null);
            Assert.Equal(locomotives.Count(), result.Count());
            Assert.Equal(locomotives[1].Name, result[0].Name);
            Assert.Equal(locomotives[2].Name, result[1].Name);
            Assert.Equal(locomotives[0].Name, result[2].Name);
        }

        [Fact]
        public void GetSingleDetailLocomotive()
        {
            // Arange
            int locomotiveId = 1;
            Locomotive locomotive = new Locomotive
            {
                ProductId = locomotiveId,
                Name = "BR 023 040-9"
            };
            DetailsLocomotiveDto result;

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "GetSingleDetailLocomotive")
                .Options;

            using (var context = new EShopContext(options))
            {
                context.Locomotives.Add(locomotive);
                context.SaveChanges();
            }

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                result = service.GetDetailsLocomotive(locomotiveId);
            }

            // Assert
            Assert.True(result != null);
            Assert.Equal(locomotive.Name, result.Name);
        }

        [Fact]
        public void EditSingleLocomotive()
        {
            // Arange
            int locomotiveId = 1;
            Locomotive originalLocomotive = new Locomotive
            {
                ProductId = locomotiveId,
                Name = "Litra MZ"
            };
            EditLocomotiveDto editLocomotive = new EditLocomotiveDto
            {
                Id = locomotiveId,
                Name = "Northlander"
            };

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "EditSingleLocomotive")
                .Options;

            using (var context = new EShopContext(options))
            {
                context.Locomotives.Add(originalLocomotive);
                context.SaveChanges();
            }

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                service.Edit(editLocomotive);
            }

            // Assert
            using (var context = new EShopContext(options))
            {
                Assert.Equal(context.Locomotives.Find(locomotiveId).Name, editLocomotive.Name);
                Assert.True(context.Locomotives.Count() == 1);
            }
        }

        [Fact]
        public void DeleteSingleLocomotive()
        {
            // Arange
            var locomotives = new List<Locomotive>()
            {
                new Locomotive { ProductId = 1, Name = "Litra MY" },
                new Locomotive { ProductId = 2, Name = "IC3" },
                new Locomotive { ProductId = 3, Name = "BR 185" },
                new Locomotive { ProductId = 4, Name = "IC4" },
                new Locomotive { ProductId = 5, Name = "BR 001" }
            };
            int result;

            var options = new DbContextOptionsBuilder<EShopContext>()
                .UseInMemoryDatabase(databaseName: "DeleteSingleLocomotive")
                .Options;

            using (var context = new EShopContext(options))
            {
                context.Locomotives.AddRange(locomotives);
                context.SaveChanges();
            }

            // Act
            using (var context = new EShopContext(options))
            {
                var service = new LocomotiveService(context);
                result = service.Delete(4);
            }

            // Assert
            Assert.True(result > 0);

            using (var context = new EShopContext(options))
            {
                bool locomotiveStillExists = context.Locomotives.Any(l => l.ProductId == 4);
                Assert.False(locomotiveStillExists);
            }
        }
    }
}
