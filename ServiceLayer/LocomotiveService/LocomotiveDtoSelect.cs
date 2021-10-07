using DataLayer.Models.Products;
using ServiceLayer.ImageService.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.LocomotiveService
{
    public static class LocomotiveDtoSelect
    {
        public static IQueryable<ListLocomotiveDto> MapListLocomotiveToDto(this IQueryable<Locomotive> locomotives)
        {
            return locomotives.Select(l => new ListLocomotiveDto
            {
                Name = l.Name,
                RailwayCompanyName = l.RailwayCompany.Name,
                Price = l.Price,
                StockStatus = new StockStatusDto
                {
                    Amount = l.StockStatus.Amount,
                    NextStock = l.StockStatus.NextStock
                },
                Images = l.Images.MapImageToDto(),
                Tag = new TagDto
                {
                    Text = l.Tag.Text
                }
            });
        }
    }
}
