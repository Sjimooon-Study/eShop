using ServiceLayer;
using ServiceLayer.LocomotiveService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor
{
    public static class TestData
    {
        public static List<ListLocomotiveDto> GetListLocomotives()
        {
            List<ListLocomotiveDto> locomotiveList = new();

            for (int i = 0; i < 20; i++)
            {
                locomotiveList.Add(new ListLocomotiveDto()
                {
                    ProductId = i,
                    Name = $"Test{i}",
                    Price = i * 10 + 0.95M,
                    AmountInStock = 5,
                    Scale = DataLayer.Models.ModelItem.EScale.HO,
                    Epoch = DataLayer.Models.ModelItem.EEpoch.IV,
                    RailwayCompanyName = "DB",
                    Images = new List<ImageDto>(),
                    Tag = "New",
                    Control = DataLayer.Models.Products.Locomotive.EControl.DC,
                    LocoType = DataLayer.Models.Products.Locomotive.ELocoType.DieselLocomotive
                });
            }

            return locomotiveList;
        }

        public static DetailsLocomotiveDto GetDetailsLocomotive() => new()
        {
            ProductId = 1,
            Name = "DetailsLocomotive",
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque semper non arcu vitae suscipit. Praesent quis feugiat orci, ac maximus felis. Suspendisse vehicula turpis eget metus consequat, vel vehicula nisi egestas. Fusce ornare, justo ac facilisis pellentesque, nunc urna egestas nulla, placerat venenatis nisl felis mattis mi. Cras non feugiat nulla. Cras vel erat dignissim, placerat augue nec, pulvinar metus. Morbi eu ante mauris. Integer fringilla erat ac tellus consequat cursus. Curabitur ac est porttitor, consectetur urna sit amet, vestibulum quam. Duis in justo mattis, convallis nisi a, congue nunc. Morbi aliquam tellus varius massa auctor, dignissim mollis turpis condimentum. Fusce volutpat neque at tincidunt pellentesque. Proin ultrices orci ac consectetur eleifend. Proin condimentum, libero eu vehicula vehicula, libero nibh rutrum ex, ut posuere purus metus eget mi. In pulvinar consequat turpis, id lacinia arcu bibendum id.",
            Price = 299.95M,
            AmountInStock = 2,
            Images = new List<ImageDto>(),
            Tag = "New",
            Scale = DataLayer.Models.ModelItem.EScale.TT,
            Epoch = DataLayer.Models.ModelItem.EEpoch.V,
            Length = 8.1f,
            NumOfAxels = 4,
            RailwayCompanyName = "ÖBB",
            RailwatCompanyCountryName = "Austria",
            Control = DataLayer.Models.Products.Locomotive.EControl.AC,
            NumOfDrivenAxels = 4
        };
    }
}
