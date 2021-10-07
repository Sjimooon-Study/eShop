using DataLayer.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ImageService.Concrete
{
    public static class ImageService
    {
        public static ICollection<ImageDto> MapImageToDto(this ICollection<Image> images)
        {
            ICollection<ImageDto> imageCollection = images.Select(i => new ImageDto
            {
                Url = i.Url
            }).ToList();

            return imageCollection;
        }
    }
}
