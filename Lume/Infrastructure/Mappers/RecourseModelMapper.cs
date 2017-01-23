using BLL.Entities;
using BLL.Services_Interface;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lume.Infrastructure.Mappers
{
    public static class ResourceModelMapper
    {
        public static ResourceViewModel ToMvcResource(this ResourceEntity bllResource)
        {
            double rating = 0;
            var _ratingService =  (IRatingService)DependencyResolver.Current.GetService(typeof (IRatingService));
            var currentRatings = _ratingService.GetByResource(bllResource.Id);
            if (currentRatings.Count() != 0)
               rating = currentRatings.Sum(r => r.Mark) / currentRatings.Count();
            return new ResourceViewModel()
            {
                Id = bllResource.Id,
                Description = bllResource.Description,
                DownloadFile = bllResource.File,
                TypeResource = (TypeResource)bllResource.id_TypeResource,
                id_User = bllResource.id_User,
                Views = bllResource.Views,
                Name = bllResource.Name,
                Size = (double)Math.Round((decimal)(bllResource.File.Length / 1048576), 1),
                Rating = rating
            };
        }

        public static ResourceEntity ToBllResource(this ResourceViewModel mvcResource)
        {
            byte[] data;
            using (Stream inputStream = mvcResource.UploadFile.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return new ResourceEntity()
            {
                Id = mvcResource.Id,
                File = data,
                Description = mvcResource.Description,
                id_TypeResource = (int)mvcResource.TypeResource,
                id_User = mvcResource.id_User,
                Views = mvcResource.Views,
                Name = mvcResource.Name
            };
        }
    }
}