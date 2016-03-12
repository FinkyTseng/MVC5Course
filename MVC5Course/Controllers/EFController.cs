using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index(bool? IsActive, string keyword)
        {
            //var product = new Product()
            //{
            //    ProductName = "BMW",
            //    Price = 20,
            //    Active = true,
            //    Stock = 10
            //};

            //db.Product.Add(product);

            //var pKey = product.ProductId;

            //var data = db.Product.Take(5).OrderByDescending(p => p.ProductId);

            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();

            if (IsActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive : false); 
            }

            if (!String.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            foreach (var item in data)
            {
                item.Price = item.Price + 1;
            }

            SaveChange();

            return View(data);
        }

        private void SaveChange()
        {
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
        }

        public ActionResult Detail(int id)
        {
            //var data = db.Product.Find(id);
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            //db.Product.Remove(item);

            //foreach (var ol in db.OrderLine.Where(p=>p.ProductId == id).ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            //foreach (var ol in item.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            db.OrderLine.RemoveRange(item.OrderLine);
            db.Product.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult QueryPlan(int num = 10)
        {
            var item = db.Product.OrderByDescending(p => p.ProductId).ToList();
            //var item = db.Database.SqlQuery<Product>(@"Select * From dbo.Product Where ProductId < @p0", num);

            return View(item);
        }
    }
}