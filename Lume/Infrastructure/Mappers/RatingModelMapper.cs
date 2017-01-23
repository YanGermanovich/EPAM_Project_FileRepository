using BLL.Entities;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class RatingModelMapper
    {
        public static RatingViewModel ToMvcRating(this RatingEntity bllRating)
        {
            return new RatingViewModel()
            {
                Id = bllRating.Id,
                id_Resource = bllRating.id_Resource,
                id_Users = bllRating.id_Users,
                Mark = bllRating.Mark,
                Date = bllRating.Date
            };
        }

        public static RatingEntity ToBllRating(this RatingViewModel mvcRating)
        {
            return new RatingEntity()
            {
                Id = mvcRating.Id,
                id_Resource = mvcRating.id_Resource,
                id_Users = mvcRating.id_Users,
                Mark = mvcRating.Mark,
                Date = mvcRating.Date
            };
        }
    }
}