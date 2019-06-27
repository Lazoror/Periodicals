using PeriodicalsFinal.Attributes;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyCustomException());
        }
    }
}
