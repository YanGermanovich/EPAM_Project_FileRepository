using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Services_Interface
{
    public interface IUserService : IService<UserEntity>
    {
        UserEntity GetByEmail(string email);
        IEnumerable<UserEntity> GetByPassword(string password);
        IEnumerable<UserEntity> GetByRole(int id_Role);     
    }
}
