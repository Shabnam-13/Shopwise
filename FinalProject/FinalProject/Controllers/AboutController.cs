using FinalProject.DAL;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
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

            VmAbout model = new VmAbout();
            model.setting = db.Setting.FirstOrDefault();
            model.testimonials = db.Testimonial.ToList();
            model.partners = db.Partner.ToList();
            return View(model);
        }
    }
}