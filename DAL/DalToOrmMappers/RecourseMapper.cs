using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
     public static class ResourceMapper
    {
         public static DalResource ToDalResource(this Resource ormResource)
        {
            return new DalResource()
            {
                Id = ormResource.Id,
                File = ormResource.Resource_File,
                Description = ormResource.Resource_Description,
                id_TypeResource = ormResource.Resource_id_TypeResource,
                id_User = ormResource.Resource_id_User,
                Views = ormResource.Resource_Views,
                Name = ormResource.Resource_Name
            };
        }

         public static Resource ToOrmResource(this DalResource dalResource)
        {
            return new Resource()
            {
                Resource_File = dalResource.File,
                Resource_Description = dalResource.Description,
                Resource_id_TypeResource = dalResource.id_TypeResource,
                Resource_id_User = dalResource.id_User,
                Resource_Views = dalResource.Views,
                Resource_Name = dalResource.Name
            };
        }
    }
}
