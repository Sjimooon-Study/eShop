using DataLayer;
using ServiceLayer.LocomotiveService;
using ServiceLayer.LocomotiveService.Concrete;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //InitializeDb();

            using (var context = new EShopContext())
            {
                var locomotiveService = new LocomotiveService(context);

                QueryOptions queryOptions = new QueryOptions
                {
                    OrderByOptions = EOrderByOptions.ByNameDesc
                };

                var result = locomotiveService.GetListLocomotives(queryOptions);

                foreach (var locomotive in result.Item1)
                {
                    Console.WriteLine("{0}, {1} (ID: {2})", locomotive.Name, locomotive.RailwayCompanyName, locomotive.ProductId);
                    Console.WriteLine($"Price: EUR {locomotive.Price}");
                    Console.WriteLine($"In stock: {locomotive.StockStatus.InStock}");
                    if (locomotive.StockStatus.NextStock.HasValue)
                    {
                        Console.WriteLine($"Next stock: {locomotive.StockStatus.NextStock}");
                    }
                    if (locomotive.Images.Any())
                    {
                        Console.WriteLine("Image URLs:");
                        int i = 0;
                        foreach (var image in locomotive.Images)
                        {
                            i++;
                            Console.WriteLine($"{i}, {image.Url}");
                        }
                    }
                    if (locomotive.Tag != null)
                    {
                        Console.WriteLine($"Tag: {locomotive.Tag}");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void InitializeDb()
        {
            using (var context = new EShopContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                Console.WriteLine("Database recreated");
            }
        }
    }
}
