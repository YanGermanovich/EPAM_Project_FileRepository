using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BLL.Entities;
using Lume.Infrastructure.Mappers;
using System.ComponentModel.DataAnnotations;

namespace Lume.Models
{
    public class SearchViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TypeResource? Type { get; set; }

        [Display(Name = "Rating more than")]
        public double? Rating_Less { get; set; }

        [Display(Name = "Rating less than")]
        public double? Rating_More { get; set; }

        [Display(Name = "Views more than")]
        public int? View_Less { get; set; }

        [Display(Name = "Views less than")]
        public int? View_More { get; set; }

        [Display(Name = "Size more than")]
        public int? Size_Less { get; set; }

        [Display(Name = "Size less than")]
        public int? Size_More { get; set; }

        public Func<ResourceViewModel, bool> GetPredicate()
        {
            return res =>
                (Name == null || res.Name.Contains(Name)) &
                    (Description ==null || res.Description.Contains(Description) ) &
                    (Type==null || res.TypeResource == Type) &
                    (Rating_Less == null || res.Rating > Rating_Less) &
                    (Rating_More == null || res.Rating < Rating_More) &
                    (View_Less == null || res.Views > View_Less) &
                    (View_More == null || res.Views < View_More) &
                    (Size_Less == null || res.DownloadFile.Length > Size_Less) &
                    (Size_More == null || res.DownloadFile.Length < Size_More);
        }

    }
}