using FinalProject.DAL;
using FinalProject.Filters;
using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace FinalProject.Controllers
{
    public class ShopingController : Controller
    {
        // GET: Shoping
        ShopwiseDB db = new ShopwiseDB();
        //Wishlist View
        public ActionResult Index()
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();
            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];

            List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
            List<ProductInfo> products = db.ProductInfos.Include("Images")
                                                        .Include("Product")
                                                        .Include("SaleBanner")
                                                        .Where(p => wishlist.Contains(p.Id.ToString()) == true)
                                                        .ToList();

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> cart = new List<string>();
            if (cookieCart != null)
            {
                cart = cookieCart.Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }

            #endregion
            

            return View(products);
        }

        public ActionResult Cart()
        {
            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];

            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }
            
            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> cart = new List<string>();
            if (cookieCart != null)
            {
                cart = cookieCart.Value.Split(',').ToList();
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

            List<ProductInfo> pros = new List<ProductInfo>();
            foreach(var item in cart)
            {
                foreach(var pro in db.ProductInfos.Include("Product")
                                                  .Include("Images")
                                                  .Include("SaleBanner")
                                                  .ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == pro.Id)
                    {
                        pro.Count = Convert.ToDecimal(item.Split('-')[1]);
                        pros.Add(pro);
                    }
                }
            }
            return View(pros);
        }

        public ActionResult Checkout()
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];

            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> cart = new List<string>();
            if (cookieCart != null)
            {
                cart = cookieCart.Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }

            #endregion
            List<ProductInfo> pros = new List<ProductInfo>();
            foreach (var item in cart)
            {
                foreach (var pro in db.ProductInfos.Include("Product").Include("Images").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == pro.Id)
                    {
                        pro.Count = Convert.ToDecimal(item.Split('-')[1]);
                        pros.Add(pro);
                    }
                }
            }
            return View(pros);
        }

        [HttpPost]
        public ActionResult Checkout(VmCheckout checkout)
        {
            string APIResponse = "Ok";
            if (APIResponse == "Ok")
            {
                User user = db.User.Find((int)Session["LoginerId"]);
                if (user.Phone == null)
                {
                    user.Phone = checkout.Phone;
                }
                Address address = new Address();
                address.UserId = user.Id;
                address.Street = checkout.Street;
                address.Country = checkout.Country;
                address.City = checkout.City;
                address.Zipcode = checkout.ZipCode;
                address.DetailedAddress = checkout.AddressDetails;
                address.IsPrimary = false;
                db.Address.Add(address);

                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.UserId = user.Id;

                db.Order.Add(order);
                db.SaveChanges();
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = order.Id;
                foreach (var item in checkout.ProductId)
                {
                    ProductInfo pro = db.ProductInfos.Find(item);
                    int index = checkout.ProductId.ToList().IndexOf(item);
                    pro.Quantity -= checkout.ProductCount[index];
                    if (pro.Quantity < 0)
                    {
                        Session["Unaviable"] = true;
                        return RedirectToAction("Checkout","Shoping");
                    }
                    orderItem.ProductInfoId= checkout.ProductId[index];
                    orderItem.Quantity = checkout.ProductCount[index];

                    db.OrderItem.Add(orderItem);
                    db.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                HttpCookie cookieMe = Request.Cookies["Cart"];
                var c = new HttpCookie("Cart");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);

                Session["ok"] = true;
                return RedirectToAction("Oreders", "User");
            }
            else
            {
                ModelState.AddModelError("", "Kartinizda kifayet qeder pul yoxdur");
            }
            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];

            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                ViewBag.Wishlist = wishlist;
            }

            HttpCookie cookieCart = Request.Cookies["Cart"];
            List<string> cart = new List<string>();
            if (cookieCart != null)
            {
                cart = cookieCart.Value.Split(',').ToList();
                cart.RemoveAt(cart.Count - 1);
                ViewBag.CartCount = cart.Count;
                ViewBag.Cart = cart;
            }
            else
            {
                ViewBag.CartCount = 0;
            }

            #endregion

            List<ProductInfo> pros = new List<ProductInfo>();
            foreach (var item in cart)
            {
                foreach (var pro in db.ProductInfos.Include("Product").Include("Images").ToList())
                {
                    if (Convert.ToInt32(item.Split('-')[0]) == pro.Id)
                    {
                        pro.Count = Convert.ToDecimal(item.Split('-')[1]);
                        pros.Add(pro);
                    }
                }
            }
            return View(pros);

        }

        public ActionResult DeleteWishlit(int? id)
        {

            if (id != null)
            {
                string oldList = Request.Cookies["WishList"].Value;
                HttpCookie cookie = new HttpCookie("WishList");
                cookie.Value = oldList;
                if (oldList.Contains(id.ToString()) == true)
                {
                    List<string> oldListArr = oldList.Split(',').ToList();
                    oldListArr.Remove(id.ToString());

                    oldList = string.Join(",", oldListArr);
                    cookie.Value = oldList;
                    Request.Cookies["WishList"].Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();

                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public JsonResult DeleteCart(int? id)
        {
            string response = "";
            if (id != null)
            {
                string oldList = Request.Cookies["Cart"].Value;
                HttpCookie cookie = new HttpCookie("Cart");
                List<string> cartList = oldList.Split(',').ToList();
                cartList.RemoveAt(cartList.Count - 1);

                string cartItem = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);
                if (cartItem != null)
                {
                    cartList.Remove(cartItem);

                    if (cartList.Count() > 0)
                    {
                        cookie.Value = string.Join(",", cartList) + ",";
                    }
                    else
                    {
                        cookie.Value = "";
                    }

                    Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                    response = "successTrue";
                }

            }
            else
            {
                response = "error";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}