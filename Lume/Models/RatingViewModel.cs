using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class RatingViewModel
    {
        public int Id { get; set; }
        public double Mark { get; set; }
        public int id_Users { get; set; }
        public int id_Resource { get; set; }
        public DateTime Date { get; set; }
    }
}