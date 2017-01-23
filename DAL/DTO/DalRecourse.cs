using DAL.DalToOrmMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExpressionVisitor;

namespace DAL.DTO
{
    public class DalResource: IEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("Resource_id_TypeResource")]
        public int id_TypeResource { get; set; }

        [CustomAttributeMapper("Resource_id_User")]       
        public int id_User { get; set; }

        [CustomAttributeMapper("Resource_File")]       
        public byte[] File { get; set; }

        [CustomAttributeMapper("Resource_Description")]
        public string Description { get; set; }

        [CustomAttributeMapper("Resource_Views")]
        public int Views { get; set; }

        [CustomAttributeMapper("Resource_Name")]
        public string Name { get; set; }
    }
}
