using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class 紀錄Action執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            TimeSpan exectionTime = TimeSpan.FromHours(1);

            filterContext.Controller.ViewBag.執行時間 = exectionTime;

            base.OnActionExecuted(filterContext);
        }
    }
}