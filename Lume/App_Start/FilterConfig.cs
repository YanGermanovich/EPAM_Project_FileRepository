﻿using Lume.Infrastructure.Filters;
using System.Web;
using System.Web.Mvc;

namespace Lume
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new NLogExceptionHandlerAttribute());
        }
    }

    
}