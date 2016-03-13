using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    [紀錄Action執行時間]
    public class HomeController : BaseController
    {
        [共用的ViewBag共享於部分HomeController動作方法]
        public ActionResult Index()
        {
            return View();
        }

        [共用的ViewBag共享於部分HomeController動作方法]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        [HandleError(ExceptionType= typeof(ArgumentException), View = "ArgumentError")]
        public ActionResult ErrorTest(string e)
        {
            switch (e)
            {
                case "1":
                    throw new Exception("Error 1");
                case "2":
                    throw new ArgumentException("Error 2");
            }

            return Content("No Error");
        }

        public ActionResult RazorTest()
        {
            int[] data = new int[] { 1, 2, 3, 4, 5 };
                
            return PartialView(data);
        }
    }
}