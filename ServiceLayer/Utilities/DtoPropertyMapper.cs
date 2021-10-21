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
        /// <summary>
        /// Map collection of <see cref="Image"/> to <see cref="ImageDto"/>. Takes null.
        /// </summary>
        /// <param name="images"></param>
        /// <returns>Collection of <see cref="ImageDto"/>.</returns>
        public static ICollection<ImageDto> MapImageToDto(this ICollection<Image> images)
        {
            if (images == null)
            {
                return new List<ImageDto>();
            }
            
            return images.Select(i => new ImageDto
            {
                Path = i.Path
            }).ToList();
        }

        /// <summary>
        /// Map collection of <see cref="Image"/> to <see cref="EditImageDto"/>. Takes null.
        /// </summary>
        /// <param name="images"></param>
        /// <returns>Collection of <see cref="EditImageDto"/>.</returns>
        public static ICollection<EditImageDto> MapEditImageToDto(this ICollection<Image> images)
        {
            if (images == null)
            {
                return new List<EditImageDto>();
            }

            return images.Select(i => new EditImageDto
            {
                ImageId = i.ImageId,
                Path = i.Path
            }).ToList();
        }
        #endregion

        #region Add
        /// <summary>
        /// Map <see cref="AddModelItemDto"/> to <see cref="ModelItem"/>.
        /// </summary>
        /// <typeparam name="T">Derivative of <see cref="ModelItem"/>.</typeparam>
        /// <param name="modelItem"><typeparamref name="T"/> to map.</param>
        /// <param name="properties">Properties to map.</param>
        /// <returns><typeparamref name="T"/> with populated properties.</returns>
        public static T MapModelItemProperties<T>(this T modelItem, AddModelItemDto properties) where T : ModelItem
        {
            modelItem.Scale = properties.Scale;
            modelItem.Epoch = properties.Epoch;

            return modelItem;
        }

        /// <summary>
        /// Map <see cref="AddRollingStockDto"/> to <see cref="RollingStock"/>.
        /// </summary>
        /// <typeparam name="T">Derivative of <see cref="RollingStock"/>.</typeparam>
        /// <param name="rollingStock"><typeparamref name="T"/> to map.</param>
        /// <param name="properties">Properties to map.</param>
        /// <returns><typeparamref name="T"/> with populated properties.</returns>
        public static T MapRollingStockProperties<T>(this T rollingStock, AddRollingStockDto properties) where T : RollingStock
        {
            rollingStock.Length = properties.Length;
            rollingStock.NumOfAxels = properties.NumOfAxels;
            rollingStock.RailwayCompanyId = properties.RailwayCompanyId;

            return rollingStock;
        }

        /// <summary>
        /// Map <see cref="AddRollingStockDto"/> to <see cref="Locomotive"/>.
        /// </summary>
        /// <typeparam name="T">Derivative of <see cref="Locomotive"/>.</typeparam>
        /// <param name="locomotive"><typeparamref name="T"/> to map.</param>
        /// <param name="properties">Properties to map.</param>
        /// <returns><typeparamref name="T"/> with populated properties.</returns>
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

        #region AddEdit
        /// <summary>
        /// Map <see cref="AddEditProductDto"/> to <see cref="Product"/>.
        /// </summary>
        /// <typeparam name="T">Derivative of <see cref="Product"/>.</typeparam>
        /// <param name="product"><typeparamref name="T"/> to map.</param>
        /// <param name="properties">Properties to map.</param>
        /// <returns><typeparamref name="T"/> with populated properties.</returns>
        public static T MapProductProperties<T>(this T product, AddEditProductDto properties) where T : Product
        {
            product.Name = properties.Name;
            product.Description = properties.Description;
            product.Price = properties.Price;
            product.Images = new List<Image>();
            product.TagId = properties.Tag;
            product.AmountInStock = properties.AmountInStock;

            // Add existing images
            foreach (EditImageDto image in properties.ReusedImages)
            {
                product.Images.Add(new Image { ImageId = image.ImageId, Path = image.Path });
            }

            // Add new images
            foreach (AddImageDto image in properties.AddedImages)
            {
                product.Images.Add(new Image { Path = image.Path });
            }

            return product;
        }
        #endregion

        #region Select
        /// <summary>
        /// Map queryable of <see cref="Locomotive"/> to queryable of <see cref="ListLocomotiveDto"/>.
        /// </summary>
        /// <param name="locomotives">Queryable to map.</param>
        /// <returns>Queryable of <see cref="ListLocomotiveDto"/> with populated properties.</returns>
        public static IQueryable<ListLocomotiveDto> MapListLocomotiveToDto(this IQueryable<Locomotive> locomotives)
        {
            return locomotives.Select(l => new ListLocomotiveDto
            {
                ProductId = l.ProductId,
                Name = l.Name,
                Price = l.Price,
                AmountInStock = l.AmountInStock,
                Images = l.Images.MapImageToDto(),
                Tag = l.Tag.TagId,
                Scale = l.Scale,
                Epoch = l.Epoch,
                RailwayCompanyName = l.RailwayCompany.Name,
                Control = l.Control,
                LocoType = l.LocoType
            });
        }

        /// <summary>
        /// Map <see cref="Locomotive"/> to <see cref="DetailsLocomotiveDto"/>.
        /// </summary>
        /// <param name="locomotive"><see cref="Locomotive"/> to map.</param>
        /// <returns>Populated <see cref="DetailsLocomotiveDto"/>.</returns>
        public static DetailsLocomotiveDto MapDetailsLocomotiveDto(this Locomotive locomotive)
        {
            return new DetailsLocomotiveDto()
            {
                ProductId = locomotive.ProductId,
                Name = locomotive.Name,
                Description = locomotive.Description,
                Price = locomotive.Price,
                AmountInStock = locomotive.AmountInStock,
                Images = locomotive.Images.MapImageToDto(),
                Tag = locomotive.TagId,
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

        /// <summary>
        /// Map <see cref="Locomotive"/> to <see cref="EditLocomotiveDto"/>.
        /// </summary>
        /// <param name="locomotive"><see cref="Locomotive"/> to map.</param>
        /// <returns><see cref="EditLocomotiveDto"/> with populated properties.</returns>
        public static EditLocomotiveDto MapEditLocomotiveDto(this Locomotive locomotive)
        {
            return new EditLocomotiveDto()
            {
                Name = locomotive.Name,
                Description = locomotive.Description,
                Price = locomotive.Price,
                AmountInStock = locomotive.AmountInStock,
                ReusedImages = locomotive.Images.MapEditImageToDto(),
                Tag = locomotive.TagId,
                Scale = locomotive.Scale,
                Epoch = locomotive.Epoch,
                Length = locomotive.Length,
                NumOfAxels = locomotive.NumOfAxels,
                RailwayCompanyId = locomotive.RailwayCompanyId,
                Id = locomotive.ProductId,
                Control = locomotive.Control,
                LocoType = locomotive.LocoType,
                AutoCoupling = locomotive.AutoCoupling,
                NumOfDrivenAxels = locomotive.NumOfDrivenAxels,
                DigitalDecoderId = locomotive.DigitalDecoderId
            };
        }
        #endregion

        #region Edit
        /// <summary>
        /// Map <see cref="EditLocomotiveDto"/> to <see cref="Locomotive"/>.
        /// </summary>
        /// <typeparam name="T">Derivative of <see cref="Locomotive"/>.</typeparam>
        /// <param name="locomotive"><typeparamref name="T"/> to map.</param>
        /// <param name="properties">Properties to map.</param>
        /// <returns><typeparamref name="T"/> with populated properties.</returns>
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
