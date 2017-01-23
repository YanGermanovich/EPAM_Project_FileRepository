using DAL.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalToOrmMappers
{
    public static class RoleMappers
    {
        public static DalRole ToDalRole(this Role otmRole)
        {
            return new DalRole()
            {
                Id = otmRole.Id,
                Name = otmRole.Roles_Name
            };
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            return new Role()
            {
                Roles_Name = dalRole.Name
            };
        }
    }
}
