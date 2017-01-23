using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.BLLEntityToDalMappers
{
    public static class RatingEntityMapper
    {
        public static DalRating ToDalRating(this RatingEntity bllRating)
        {
            return new DalRating()
            {
                Id = bllRating.Id,
                id_Resource = bllRating.id_Resource,
                id_Users = bllRating.id_Users,
                Mark = bllRating.Mark,
                Date = bllRating.Date
            };
        }

        public static RatingEntity ToBllRating(this DalRating dalRating)
        {
            return new RatingEntity()
            {
                Id = dalRating.Id,
                id_Resource = dalRating.id_Resource,
                id_Users = dalRating.id_Users,
                Mark = dalRating.Mark,
                Date = dalRating.Date
            };
        }
    }
}
