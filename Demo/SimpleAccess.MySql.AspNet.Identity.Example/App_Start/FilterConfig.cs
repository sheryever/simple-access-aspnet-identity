using System.Web;
using System.Web.Mvc;

namespace SimpleAccess.MySql.AspNet.Identity.Example
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
