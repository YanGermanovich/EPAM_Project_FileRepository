using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lume.Models
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public string ActionName { get; set; }
    }
}