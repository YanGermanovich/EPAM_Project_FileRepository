using BLL.BLLEntityToDalMappers;
using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class RatingEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("Mark")]
        public double Mark { get; set; }

        [CustomAttributeMapper("id_Users")]
        public int id_Users { get; set; }

        [CustomAttributeMapper("id_Resource")]
        public int id_Resource { get; set; }

        [CustomAttributeMapper("Date")]
        public System.DateTime Date { get; set; }
    }
}
