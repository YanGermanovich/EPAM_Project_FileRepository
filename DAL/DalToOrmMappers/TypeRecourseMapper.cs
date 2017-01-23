using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
     public static class TypeResourceMapper
    {
        public static DalTypeResource ToDalTypeResource(this TypeResource ormTypeResource)
        {
            return new DalTypeResource()
            {
                Id = ormTypeResource.Id,
                TypeResource_name = ormTypeResource.TypeResource_name
            };
        }

        public static TypeResource ToOrmTypeResource(this DalTypeResource dalTypeResource)
        {
            return new TypeResource()
            {
                TypeResource_name = dalTypeResource.TypeResource_name
            };
        }
    }
}
