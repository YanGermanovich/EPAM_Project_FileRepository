using BLL.Entities;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Infrastructure.Mappers
{
    public static class UserModelMapper
    {
        public static UserViewModel ToMvcUser(this UserEntity bllUser)
        {
            return new UserViewModel()
            {
                Id = bllUser.Id,
                Email = bllUser.Email,
                Password = bllUser.Password,
                Role = (Role) bllUser.id_Role,
                Creation_Date = bllUser.Creation_Date
            };
        }

        public static UserEntity ToBllUser(this UserViewModel mvcUser)
        {
            return new UserEntity()
            {
                Id = mvcUser.Id,
                Email = mvcUser.Email,
                Password = mvcUser.Password,
                id_Role = (int)mvcUser.Role,
                Creation_Date = mvcUser.Creation_Date
            };
        }
    }
}