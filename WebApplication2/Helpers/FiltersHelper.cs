﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2
{
    public class FiltersHelper : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(!filterContext.ExceptionHandled && filterContext.Exception is DivideByZeroException)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = @"~/Views/Shared/Error.cshtml"
                };
                    //new RedirectResult("~/Views/Shared/Error.cshtml");

                
                filterContext.ExceptionHandled = true;

            }
        }
    }
}