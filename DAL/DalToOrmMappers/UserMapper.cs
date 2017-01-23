using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this User ormUser)
        {
            return new DalUser()
            {
                Id = ormUser.Id,
                Email = ormUser.Users_Email,
                Password = ormUser.Users_Password,
                id_Role = ormUser.Users_id_Role,
                CreationDate = ormUser.Users_Creation_Date
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User()
            {   
                Users_Email = dalUser.Email,
                Users_Password = dalUser.Password,
                Users_id_Role = dalUser.id_Role,
                Users_Creation_Date = dalUser.CreationDate
            };
        }
    }
}
