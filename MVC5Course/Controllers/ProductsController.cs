using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        //ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(string type, int? id, bool? isActive)
        {
            //OrderLineRepository repoOL = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            if (id.HasValue)
            {
                ViewBag.SelectedId = id;
            }

            var data = repo.All(true);

            if (isActive != null)
            {
                data = data.Where(p => p.Active.Value && p.Active == isActive);
            }

            List<SelectListItem> item = new List<SelectListItem>();
            item.Add(new SelectListItem() { Value = "true", Text = "有效" });
            item.Add(new SelectListItem() { Value = "false", Text = "無效" });
            ViewBag.isActive = new SelectList(item, "Value", "Text");

            ViewBag.type = type;

            return View(data.Take(10));
        }

        [HttpPost]
        public ActionResult Index(IList<Product批次更新ViewModel> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var product = repo.Find(item.ProductId);
                    product.Price = item.Price;
                    product.Stock = item.Stock;
                }
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(repo.All().Take(5));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            var product = repo.Find(id);

            if (TryUpdateModel<IProduct>(product))
            {
                repo.UnitOfWork.Commit();

                TempData["ProductEditMsg"] = "產品新增成功";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = repo.Find(id);
            product.IsDeleted = true;
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
