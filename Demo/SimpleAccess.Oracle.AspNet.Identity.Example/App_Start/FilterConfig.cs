﻿using System.Web;
using System.Web.Mvc;

namespace SimpleAccess.Oracle.AspNet.Identity.Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
