using DataLayer.Models.Products;
using DataLayer.Models;
using ServiceLayer.LocomotiveService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ServiceLayer
{
    public static class DtoPropertyMapper
    {
        #region To DTO
        public static ICollection<ImageDto> MapImageToDto(this ICollection<Image> images)
        {
            ICollection<ImageDto> imageCollection = images.Select(i => new ImageDto
            {
                Url = i.Url
            }).ToList();

            return imageCollection;
        }
        #endregion

        #region Select
        public static IQueryable<ListLocomotiveDto> MapListLocomotiveToDto(this IQueryable<Locomotive> locomotives)
        {
            return locomotives.Select(l => new ListLocomotiveDto
            {
                ProductId = l.ProductId,
                Name = l.Name,
                RailwayCompanyName = l.RailwayCompany.Name,
                Price = l.Price,
                Scale = l.Scale,
                StockStatus = new StockStatusDto
                {
                    Amount = l.StockStatus.Amount,
                    NextStock = l.StockStatus.NextStock
                },
                Images = l.Images.MapImageToDto(),
                Tag = l.Tag.TagId
            });
        }
        #endregion

        #region Add
        public static T MapProductProperties<T>(this T product, AddProductDto properties) where T : Product
        {
            product.Name = properties.Name;
            product.Description = properties.Description;
            product.Price = properties.Price;
            product.Images = new List<Image>();
            product.TagId = properties.Tag;
            product.StockStatus = new StockStatus
            {
                Amount = properties.StockStatus.Amount,
                NextStock = properties.StockStatus.NextStock
            };

            // Add existing images
            foreach (int imageId in properties.ReusedImages)
            {
                product.Images.Add(new Image { ImageId = imageId });
            }
            
            // Add new images
            foreach (AddImageDto image in properties.AddedImages)
            {
                product.Images.Add(new Image { Url = image.Url });
            }

            return product;
        }

        public static T MapModelItemProperties<T>(this T modelItem, AddModelItemDto properties) where T : ModelItem
        {
            modelItem.Scale = properties.Scale;
            modelItem.Epoch = properties.Epoch;

            return modelItem;
        }

        public static T MapRollingStockProperties<T>(this T rollingStock, AddRollingStockDto properties) where T : RollingStock
        {
            rollingStock.Length = properties.Length;
            rollingStock.NumOfAxels = properties.NumOfAxels;
            rollingStock.RailwayCompanyId = properties.RailwayCompanyId;

            return rollingStock;
        }

        public static T MapLocomotiveProperties<T>(this T locomotive, AddLocomotiveDto properties) where T : Locomotive
        {
            locomotive.Control = properties.Control;
            locomotive.LocoType = properties.LocoType;
            locomotive.AutoCoupling = properties.AutoCoupling;
            locomotive.NumOfDrivenAxels = properties.NumOfDrivenAxels;
            locomotive.DigitalDecoderId = properties.DigitalDecoderId;

            return locomotive;
        }
        #endregion
    }
}
