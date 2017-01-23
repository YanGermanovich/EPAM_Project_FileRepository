using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public enum TypeResource
    {
        Audio = 0,
        Video,
        Text
    }

    public class ResourceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter resource type")]
        public TypeResource TypeResource { get; set; }
        public int id_User { get; set; }

        [Display(Name = "File")]
        public HttpPostedFileBase UploadFile { get; set; }
        public byte[] DownloadFile{ get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public int Views { get; set; }
        public double Rating { get; set; 
        }
        [Display(Name="Name")]
        [Required(ErrorMessage="Please enter resource name")]
        public string Name { get; set; }
        public double Size { get; set; }
    }
}