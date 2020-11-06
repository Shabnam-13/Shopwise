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
    public class BlogTagsController : Controller
    {
        private ShopwiseDB db = new ShopwiseDB();

        // GET: Admin/BlogTags
        public ActionResult Index()
        {
            return View(db.BlogTags.ToList());
        }


        // GET: Admin/BlogTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BlogTags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] BlogTags blogTags)
        {
            if (ModelState.IsValid)
            {
                db.BlogTags.Add(blogTags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogTags);
        }

        // GET: Admin/BlogTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTags blogTags = db.BlogTags.Find(id);
            if (blogTags == null)
            {
                return HttpNotFound();
            }
            return View(blogTags);
        }

        // POST: Admin/BlogTags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] BlogTags blogTags)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogTags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogTags);
        }

        // POST: Admin/BlogTags/Delete/5
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogTags BlogTags = db.BlogTags.Find(id);
            if (BlogTags == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.BlogTags.Remove(BlogTags);
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
