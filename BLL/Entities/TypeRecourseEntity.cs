using BLL.BLLEntityToDalMappers;
using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class TypeResourceEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("TypeResource_name")]
        public string TypeResource_name { get; set; }
    }
}
