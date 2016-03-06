using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        // GET: EF
        public ActionResult Index()
        {
            var db = new FabricsEntities();

            db.Product.Add(new Product() {
                ProductName = "BMW",
                Price = 1,
                Active = true,
                Stock = 10
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(err.PropertyName + "驗證失敗。(" + err.ErrorMessage + ")");
                    }
                }
                throw;
            }

            var data = db.Product.ToList();

            return View(data);
        }
    }
}