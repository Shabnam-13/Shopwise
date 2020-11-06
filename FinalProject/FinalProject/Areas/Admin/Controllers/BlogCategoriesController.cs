using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class BlogCategoriesController : Controller
    {
        private ShopwiseDB db = new ShopwiseDB();

        // GET: Admin/BlogCategories
        public ActionResult Index()
        {
            return View(db.BlogCategory.ToList());
        }

        // GET: Admin/BlogCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                db.BlogCategory.Add(blogCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogCategory);
        }

        // GET: Admin/BlogCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogCategory blogCategory = db.BlogCategory.Find(id);
            if (blogCategory == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogCategory);
        }

        // POST: Admin/BlogCategories/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            BlogCategory blogCategory = db.BlogCategory.Find(id);
            if (blogCategory == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.BlogCategory.Remove(blogCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
