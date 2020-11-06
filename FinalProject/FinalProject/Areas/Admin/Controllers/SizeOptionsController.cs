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
    public class SizeOptionsController : Controller
    {
        private ShopwiseDB db = new ShopwiseDB();

        // GET: Admin/SizeOptions
        public ActionResult Index()
        {
            return View(db.SizeOption.ToList());
        }

        // GET: Admin/SizeOptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeOption sizeOption = db.SizeOption.Find(id);
            if (sizeOption == null)
            {
                return HttpNotFound();
            }
            return View(sizeOption);
        }

        // GET: Admin/SizeOptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SizeOptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Key,Value")] SizeOption sizeOption)
        {
            if (ModelState.IsValid)
            {
                db.SizeOption.Add(sizeOption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sizeOption);
        }

        // GET: Admin/SizeOptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeOption sizeOption = db.SizeOption.Find(id);
            if (sizeOption == null)
            {
                return HttpNotFound();
            }
            return View(sizeOption);
        }

        // POST: Admin/SizeOptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Key,Value")] SizeOption sizeOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sizeOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sizeOption);
        }

        // GET: Admin/SizeOptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SizeOption sizeOption = db.SizeOption.Find(id);
            if (sizeOption == null)
            {
                return HttpNotFound();
            }
            return View(sizeOption);
        }

        // POST: Admin/SizeOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SizeOption sizeOption = db.SizeOption.Find(id);
            db.SizeOption.Remove(sizeOption);
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
