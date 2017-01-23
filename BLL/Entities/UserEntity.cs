using CustomExpressionVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    public class UserEntity
    {
        [CustomAttributeMapper("Id")]
        public int Id { get; set; }

        [CustomAttributeMapper("Email")]
        public string Email { get; set; }

        [CustomAttributeMapper("Password")]
        public string Password { get; set; }

        [CustomAttributeMapper("id_Role")]
        public int id_Role { get; set; }

        [CustomAttributeMapper("Creation_Date")]
        public DateTime Creation_Date { get; set; }
    }
}
