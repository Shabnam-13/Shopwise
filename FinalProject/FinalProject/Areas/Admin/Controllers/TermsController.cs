using FinalProject.DAL;
using FinalProject.Areas.Admin.Filters;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class TermsController : Controller
    {
        // GET: Admin/Terms
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Term> terms = db.Term.ToList();
            return View(terms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Term term)
        {
            if (ModelState.IsValid)
            {
                db.Term.Add(term);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(term);
        }

        public ActionResult Update(int? id)
        {
            Term term = db.Term.Find(id);
            if (term == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(term);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Term term)
        {
            if (ModelState.IsValid)
            {
                db.Entry(term).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(term);
        }

        public ActionResult Delete(int? id)
        {
            Term term = db.Term.Find(id);
            if (term == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Term.Remove(term);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}