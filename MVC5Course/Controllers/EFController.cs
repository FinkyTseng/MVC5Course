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
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var product = new Product()
            {
                ProductName = "BMW",
                Price = 20,
                Active = true,
                Stock = 10
            };

            db.Product.Add(product);

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

            var pKey = product.ProductId;

            var data = db.Product.Take(20).OrderByDescending(p => p.ProductId);

            return View(data);
        }

        public ActionResult Detail(int id)
        {
            //var data = db.Product.Find(id);
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }
    }
}