using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.BLLEntityToDalMappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity bllUser)
        {
            return new DalUser()
            {
                Id = bllUser.Id,
                Email = bllUser.Email,
                Password = bllUser.Password,
                id_Role = bllUser.id_Role,
                CreationDate = bllUser.Creation_Date
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Email = dalUser.Email,
                Password = dalUser.Password,
                id_Role = dalUser.id_Role,
                Creation_Date = dalUser.CreationDate
            };
        }
    }
}
