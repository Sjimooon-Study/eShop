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
        #region General
        public static ICollection<ImageDto> MapImageToDto(this ICollection<Image> images)
        {
            return images?.Select(i => new ImageDto
            {
                Url = i.Url
            }).ToList();
        }

        public static StockStatusDto MapStockStatusDto(this StockStatus stockStatus)
        {
            if (stockStatus != null)
            {
                return new StockStatusDto
                {
                    Amount = stockStatus.Amount,
                    NextStock = stockStatus.NextStock
                };
            }
            
            return null;
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
                StockStatus = l.StockStatus.MapStockStatusDto(),
                Images = l.Images.MapImageToDto(),
                Tag = l.Tag.TagId
            });
        }

        public static DetailsLocomotiveDto MapDetailsLocomotiveDto(this Locomotive locomotive)
        {
            return new DetailsLocomotiveDto()
            {
                ProductId = locomotive.ProductId,
                Name = locomotive.Name,
                Description = locomotive.Description,
                Price = locomotive.Price,
                Images = locomotive.Images.MapImageToDto(),
                Tag = locomotive.Tag?.TagId,
                StockStatus = locomotive.StockStatus.MapStockStatusDto(),
                Scale = locomotive.Scale,
                Epoch = locomotive.Epoch,
                Length = locomotive.Length,
                NumOfAxels = locomotive.NumOfAxels,
                RailwayCompanyName = locomotive.RailwayCompany?.Name,
                RailwatCompanyCountryName = locomotive.RailwayCompany?.Country.Name,
                Control = locomotive.Control,
                LocoType = locomotive.LocoType,
                AutoCoupling = locomotive.AutoCoupling,
                NumOfDrivenAxels = locomotive.NumOfDrivenAxels
            };
        }
        #endregion

        #region Edit
        public static T MapLocomotiveProperties<T>(this T locomotive, EditLocomotiveDto properties) where T : Locomotive
        {
            locomotive.ProductId = properties.Id;
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
