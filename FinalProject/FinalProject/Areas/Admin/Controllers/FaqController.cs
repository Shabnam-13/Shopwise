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
    public class FaqController : Controller
    {
        // GET: Admin/Faq
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Question> terms = db.Question.ToList();
            return View(terms);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Question.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        public ActionResult Update(int? id)
        {
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(question);
        }

        [HttpPost]
        public ActionResult Update(Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        public ActionResult Delete(int? id)
        {
            Question question = db.Question.Find(id);
            if (question == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Question.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}