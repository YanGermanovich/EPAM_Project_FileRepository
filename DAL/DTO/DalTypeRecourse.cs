using CustomExpressionVisitor;
using DAL.DalToOrmMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalTypeResource: IEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("TypeResource_name")]
        public string TypeResource_name { get; set; }
    }
}
