using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
     public static class RatingMapper
    {
        public static DalRating ToDalRating(this Rating ormRating)
        {
            return new DalRating()
            {
                Id = ormRating.Id,
                id_Resource = ormRating.Ratings_id_Resource,
                id_Users = ormRating.Ratings_id_Users,
                Mark = ormRating.Ratings_Mark,
                Date = ormRating.Rating_Date
            };
        }

        public static Rating ToOrmRating(this DalRating dalRating)
        {
            return new Rating()
            {
                Ratings_id_Resource = dalRating.id_Resource,
                Ratings_id_Users = dalRating.id_Users,
                Ratings_Mark = dalRating.Mark,
                Rating_Date = dalRating.Date
            };
        }
    }
}
