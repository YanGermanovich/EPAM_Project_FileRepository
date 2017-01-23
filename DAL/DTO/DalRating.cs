using CustomExpressionVisitor;
using DAL.DalToOrmMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalRating: IEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("Ratings_Mark")]
        public double Mark { get; set; }

        [CustomAttributeMapper("Ratings_id_Users")]
        public int id_Users { get; set; }

        [CustomAttributeMapper("Ratings_id_Resource")]
        public int id_Resource { get; set; }

        [CustomAttributeMapper("Rating_Date")]
        public System.DateTime Date { get; set; }
    }
}
