using FinalProject.DAL;
using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        ShopwiseDB db = new ShopwiseDB();
        public ActionResult Index()
        {
            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            if (cookieCart != null)
            {
                List<string> cart = Request.Cookies["Cart"].Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }
            #endregion
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();


            VmSite model = new VmSite();
            model.contact = db.Contact.FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Message(VmMessage message)
        {
            if (!string.IsNullOrEmpty(message.Email) && !string.IsNullOrEmpty(message.Name))
            {
                if (!string.IsNullOrEmpty(message.Content))
                {
                    Message msg = new Message();
                    msg.Name = message.Name;
                    msg.Email = message.Email;
                    msg.Phone = message.Phone;
                    msg.Subject = message.Subject;
                    msg.Messages = message.Content;
                    msg.SendDate = DateTime.Now;

                    db.Message.Add(msg);
                    db.SaveChanges();
                    Session["Successfull"] = true;
                    return RedirectToAction("Index");
                }
                Session["EmptySubject"] = true;
                return RedirectToAction("Index");
            }

            Session["EmptyEmailMsg"] = true;
            return RedirectToAction("Index");
        }
    }
}