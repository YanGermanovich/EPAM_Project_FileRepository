//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource
    {
        public Resource()
        {
            this.Ratings = new HashSet<Rating>();
        }
    
        public int Id { get; set; }
        public int Resource_id_TypeResource { get; set; }
        public int Resource_id_User { get; set; }
        public byte[] Resource_File { get; set; }
        public string Resource_Description { get; set; }
        public int Resource_Views { get; set; }
        public string Resource_Name { get; set; }
    
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual TypeResource TypeResource { get; set; }
        public virtual User User { get; set; }
    }
}
