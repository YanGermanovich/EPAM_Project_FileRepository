using BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services_Interface
{
    public interface IRoleService : IService<RoleEntity>
    {
        RoleEntity GetByName(string Name);   
    }
}
