using FinalProject.Areas.Admin.Filters;
using FinalProject.DAL;
using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Areas.Admin.Controllers
{
    [Logout]
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Contact> contacts = db.Contact.ToList();
            return View(contacts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Update(int? id)
        {
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(contact);
        }

        [HttpPost]
        public ActionResult Update(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Delete(int? id)
        {
            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Contact.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}