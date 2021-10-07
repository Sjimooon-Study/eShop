using DataLayer;
using ServiceLayer.LocomotiveService.Concrete;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeDb();

            //using (var context = new EShopContext())
            //{
            //    var locomotiveService = new LocomotiveService(context);

            //    var locomotives = locomotiveService.GetListLocomotives();

            //    foreach (var locomotive in locomotives)
            //    {
            //        Console.WriteLine("# {0}, {1} #", locomotive.Name, locomotive.RailwayCompanyName);
            //        Console.WriteLine($"Price: €{locomotive.Price}");
            //        Console.WriteLine($"In stock: {locomotive.StockStatus.InStock}");
            //        if (locomotive.StockStatus.NextStock.HasValue)
            //        {
            //            Console.WriteLine($"Next stock: {locomotive.StockStatus.NextStock}");
            //        }
            //        if (locomotive.Images.Any())
            //        {
            //            Console.WriteLine("Image URLs:");
            //            int i = 0;
            //            foreach (var image in locomotive.Images)
            //            {
            //                i++;
            //                Console.WriteLine($"{i}, {image.Url}");
            //            }
            //        }
            //        if (locomotive.Tag != null)
            //        {
            //            Console.WriteLine($"Tag: {locomotive.Tag}");
            //        }
            //    }
            //}
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
