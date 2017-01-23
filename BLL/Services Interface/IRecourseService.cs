using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Services_Interface
{
    public interface IResourceService : IService<ResourceEntity>
    {
        IEnumerable<ResourceEntity> GetByUser(int id_user);
        IEnumerable<ResourceEntity> GetByTypeResource(int id_typeResource);
        IEnumerable<ResourceEntity> GetByFile(byte[] file);
        IEnumerable<ResourceEntity> GetByDescription(string description);
    }
}
