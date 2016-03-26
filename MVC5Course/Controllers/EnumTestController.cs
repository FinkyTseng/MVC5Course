using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EnumTestController : Controller
    {
        // GET: EnumTest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyEnum()
        {
            ViewData.Model = new EnumViewModel()
            {
                id = 1,
                name = "Finky",
                status = status.A
            };

            return View();
        }

        public ActionResult MyEnumView()
        {
            ViewData.Model = new EnumViewModel()
            {
                id = 1,
                name = "Finky",
                status = status.A
            };

            return View();
        }
    }
}