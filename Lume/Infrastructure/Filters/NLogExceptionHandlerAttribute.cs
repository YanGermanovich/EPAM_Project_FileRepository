using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lume.Infrastructure.Filters
{
    public class NLogExceptionHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            Debug.WriteLine(filterContext.Exception.ToString());
            base.OnException(filterContext);
        }
    }
}