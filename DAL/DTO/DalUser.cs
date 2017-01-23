using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DalToOrmMappers;
using CustomExpressionVisitor;

namespace DAL.DTO
{
    public class DalUser : IEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("Users_Email")]
        public string Email { get; set; }

        [CustomAttributeMapper("Users_id_Role")]
        public int id_Role { get; set; }

        [CustomAttributeMapper("Users_Password")]
        public string Password { get; set; }

        [CustomAttributeMapper("Users_Creation_Date")]
        public DateTime CreationDate { get; set; }
    }
}
