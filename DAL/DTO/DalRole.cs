using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class DalRole: IEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id {get; set;}

        [CustomAttributeMapper("Roles_Name")]
        public string Name { get; set; }
    }
}
