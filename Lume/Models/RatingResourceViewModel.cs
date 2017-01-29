using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class RatingResourceViewModel
    {
        [Required(ErrorMessage = "Please enter rating")]
        [Range(0.0,5.0,ErrorMessage="Rating must be from 0 to 5")]
        public double? mark { get; set; }

        public int id_resource { get; set;}
    }
}