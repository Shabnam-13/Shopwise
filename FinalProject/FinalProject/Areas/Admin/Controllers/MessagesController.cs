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
    public class MessagesController : Controller
    {
        // GET: Admin/Messages
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            List<Message> sms = db.Message.ToList();
            return View(sms);
        }

        public ActionResult Delete(int? id)
        {
            Message sms = db.Message.Find(id);
            if (sms == null)
            {
                return RedirectToAction("Error", "Home");
            }
            db.Message.Remove(sms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}