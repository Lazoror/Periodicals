using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PeriodicalsFinal.DataAccess.Filters
{
    public class MyCustomException : FilterAttribute, IExceptionFilter
    {
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled &&
            exceptionContext.Exception is NullReferenceException)
            {
                exceptionContext.Result =
                new RedirectResult("/Error/Index");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}
