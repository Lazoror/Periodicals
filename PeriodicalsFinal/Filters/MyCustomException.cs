using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PeriodicalsFinal.Filters
{
    public class MyCustomException : FilterAttribute, IExceptionFilter
    {
        private readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled &&
            exceptionContext.Exception is NullReferenceException)
            {
                _Logger.Trace($"URL:{exceptionContext.HttpContext.Request.Url} \n Stack Trace: {exceptionContext.Exception}");
                exceptionContext.Result =
                new RedirectResult($"/Error/HttpError404");
                exceptionContext.ExceptionHandled = true;
            }
        }

    }
}
