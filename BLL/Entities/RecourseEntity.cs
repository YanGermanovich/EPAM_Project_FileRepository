using BLL.BLLEntityToDalMappers;
using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class ResourceEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("id_TypeResource")]
        public int id_TypeResource { get; set; }

        [CustomAttributeMapper("id_User")]
        public int id_User { get; set; }

        [CustomAttributeMapper("File")]
        public byte[] File { get; set; }

        [CustomAttributeMapper("Description")]
        public string Description { get; set; }

        [CustomAttributeMapper("Views")]
        public int Views { get; set; }

        [CustomAttributeMapper("Name")]
        public string Name { get; set; }
    }

}
