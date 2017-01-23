using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.BLLEntityToDalMappers
{
    public static class TypeResourceEntityMaper
    {
        public static DalTypeResource ToDalTypeResource(this TypeResourceEntity BllTypeResource)
        {
            return new DalTypeResource()
            {
                Id = BllTypeResource.Id,
                TypeResource_name = BllTypeResource.TypeResource_name
            };
        }

        public static TypeResourceEntity ToBllTypeResource(this DalTypeResource dalTypeResource)
        {
            return new TypeResourceEntity()
            {
                Id = dalTypeResource.Id,
                TypeResource_name = dalTypeResource.TypeResource_name
            };
        }
    }
}
