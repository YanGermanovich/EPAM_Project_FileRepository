using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.BLLEntityToDalMappers
{
    public static class ResourceEntityMapper
    {
        public static DalResource ToDalResource(this ResourceEntity bllResource)
        {
            return new DalResource()
            {
                Id = bllResource.Id,
                File = bllResource.File,
                Description = bllResource.Description,
                id_TypeResource = bllResource.id_TypeResource,
                id_User = bllResource.id_User,
                Views = bllResource.Views,
                Name = bllResource.Name
            };
        }

        public static ResourceEntity ToBllResource(this DalResource dalResource)
        {
            return new ResourceEntity()
            {
                Id = dalResource.Id,
                File = dalResource.File,
                Description = dalResource.Description,
                id_TypeResource = dalResource.id_TypeResource,
                id_User = dalResource.id_User,
                Views = dalResource.Views,
                Name = dalResource.Name
            };
        }
    }
}
