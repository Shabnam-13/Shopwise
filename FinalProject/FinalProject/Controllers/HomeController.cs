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
    public class HomeController : Controller
    {
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
            ViewBag.Contact = db.Contact.FirstOrDefault();
            ViewBag.Socials = db.SocialNet.ToList();
            ViewBag.Setting = db.Setting.FirstOrDefault();
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();
            VmHome model = new VmHome();
            model.Categories = db.Category.ToList();
            model.Sales = db.SaleBanner.ToList();
            model.productInfos = db.ProductInfos.Include("Images")
                                                .Include("Product")
                                                .Include("SaleBanner")
                                                .Include("Size")
                                                .Include("Color")
                                                .Include("sizeOptions")
                                                .ToList();
            model.Blogs = db.Blog.Include("Tags")
                                 .Include("Images")
                                 .Include("Comments")
                                 .ToList();
            model.setting = db.Setting.FirstOrDefault();
            model.Products = db.Product.ToList();
            return View(model);
        }

        public ActionResult SaleProduct(int? id)
        {
            if (id != null)
            {
                SaleBanner sale = db.SaleBanner.Find(id);
                if(sale != null)
                {
                    if (sale.StartDate < DateTime.Now && sale.EndDate > DateTime.Now)
                    {
                        VmProduct model = new VmProduct();
                        model.ProductInfos = db.ProductInfos.Include("Images")
                                                .Include("Product")
                                                .Include("SaleBanner")
                                                .Include("Size")
                                                .Include("Color")
                                                .Include("sizeOptions").Where(s => s.SaleBannerId == id).ToList();
                        return View(model);
                    }
                    else
                    {
                        return RedirectToAction("index", "Product");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public ActionResult Subscribe(VmSubscribe subscribe)
        {
            if (!string.IsNullOrEmpty(subscribe.Email))
            {
                if (db.Subscribe.Any(e => e.Email == subscribe.Email))
                {
                    Session["SameEmail"] = true;
                    if (subscribe.Page == "Home")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (subscribe.Page == "About")
                    {
                        return RedirectToAction("Index", "About");
                    }
                    else if (subscribe.Page == "ProCat")
                    {
                        return RedirectToAction("Category", "Product");
                    }
                    else if (subscribe.Page == "SubCat")
                    {
                        return RedirectToAction("Subcategory", "Product");
                    }
                    else if (subscribe.Page == "SubofSubCat")
                    {
                        return RedirectToAction("SubOfSubcategory", "Product");
                    }
                    else if (subscribe.Page == "BlogCat")
                    {
                        return RedirectToAction("Categories", "Blogs");
                    }
                    else if (subscribe.Page == "BlogTag")
                    {
                        return RedirectToAction("Tags", "Blogs");
                    }
                    else if (subscribe.Page == "Cart")
                    {
                        return RedirectToAction("Cart", "Shoping");
                    }
                    else if (subscribe.Page == "Wishlist")
                    {
                        return RedirectToAction("Index", "Shoping");
                    }
                    else if (subscribe.Page == "Product")
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else if (subscribe.Page == "ProductDetails")
                    {
                        return RedirectToAction("Details", "Product", new { id = subscribe.ItemId });
                    }
                    else if (subscribe.Page == "Blog")
                    {
                        return RedirectToAction("Index", "Blogs");
                    }
                    else if (subscribe.Page == "BlogDetails")
                    {
                        return RedirectToAction("Details", "Blogs", new { id = subscribe.ItemId });
                    }
                    else if (subscribe.Page == "Faq")
                    {
                        return RedirectToAction("Index", "Faq");
                    }
                    else if (subscribe.Page == "Terms")
                    {
                        return RedirectToAction("Index", "Terms", new { id = subscribe.ItemId });
                    }
                    else if (subscribe.Page == "Contact")
                    {
                        return RedirectToAction("Index", "Contact");
                    }
                    else if (subscribe.Page == "Checkout")
                    {
                        return RedirectToAction("Index", "Checkout");
                    }
                    else if (subscribe.Page == "User")
                    {
                        return RedirectToAction("Index", "User", new { id = subscribe.ItemId });
                    }
                    else if (subscribe.Page == "Login")
                    {
                        return RedirectToAction("Login", "User");
                    }
                    else if (subscribe.Page == "Register")
                    {
                        return RedirectToAction("Register", "User");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                Subscribe Subscribe = new Subscribe();
                Subscribe.Email = subscribe.Email;
                Subscribe.SendDate = DateTime.Now;

                db.Subscribe.Add(Subscribe);
                db.SaveChanges();

                Session["SuccessfullSubscribe"] = true;
                if (subscribe.Page == "Home")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (subscribe.Page == "About")
                {
                    return RedirectToAction("Index", "About");
                }
                else if (subscribe.Page == "ProCat")
                {
                    return RedirectToAction("Category", "Product");
                }
                else if (subscribe.Page == "SubCat")
                {
                    return RedirectToAction("Subcategory", "Product");
                }
                else if (subscribe.Page == "SubofSubCat")
                {
                    return RedirectToAction("SubOfSubcategory", "Product");
                }
                else if (subscribe.Page == "BlogCat")
                {
                    return RedirectToAction("Categories", "Blogs");
                }
                else if (subscribe.Page == "BlogTag")
                {
                    return RedirectToAction("Tags", "Blogs");
                }
                else if (subscribe.Page == "Cart")
                {
                    return RedirectToAction("Cart", "Shoping");
                }
                else if (subscribe.Page == "Wishlist")
                {
                    return RedirectToAction("Index", "Shoping");
                }
                else if (subscribe.Page == "Product")
                {
                    return RedirectToAction("Index", "Product");
                }
                else if (subscribe.Page == "ProductDetails")
                {
                    return RedirectToAction("Details", "Product", new { id = subscribe.ItemId });
                }
                else if (subscribe.Page == "Blog")
                {
                    return RedirectToAction("Index", "Blogs");
                }
                else if (subscribe.Page == "BlogDetails")
                {
                    return RedirectToAction("Details", "Blogs", new { id = subscribe.ItemId });
                }
                else if (subscribe.Page == "Faq")
                {
                    return RedirectToAction("Index", "Faq");
                }
                else if (subscribe.Page == "Terms")
                {
                    return RedirectToAction("Index", "Terms", new { id = subscribe.ItemId });
                }
                else if (subscribe.Page == "Contact")
                {
                    return RedirectToAction("Index", "Contact");
                }
                else if (subscribe.Page == "Checkout")
                {
                    return RedirectToAction("Index", "Checkout");
                }
                else if (subscribe.Page == "User")
                {
                    return RedirectToAction("Index", "User", new { id = subscribe.ItemId });
                }
                else if (subscribe.Page == "Login")
                {
                    return RedirectToAction("Login", "User");
                }
                else if (subscribe.Page == "Register")
                {
                    return RedirectToAction("Register", "User");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            Session["EmptyEmail"] = true;
            if (subscribe.Page == "Home")
            {
                return RedirectToAction("Index", "Home");
            }
            else if (subscribe.Page == "About")
            {
                return RedirectToAction("Index", "About");
            }
            else if (subscribe.Page == "ProCat")
            {
                return RedirectToAction("Category", "Product");
            }
            else if (subscribe.Page == "SubCat")
            {
                return RedirectToAction("Subcategory", "Product");
            }
            else if (subscribe.Page == "SubofSubCat")
            {
                return RedirectToAction("SubOfSubcategory", "Product");
            }
            else if (subscribe.Page == "BlogCat")
            {
                return RedirectToAction("Categories", "Blogs");
            }
            else if (subscribe.Page == "BlogTag")
            {
                return RedirectToAction("Tags", "Blogs");
            }
            else if (subscribe.Page == "Cart")
            {
                return RedirectToAction("Cart", "Shoping");
            }
            else if (subscribe.Page == "Wishlist")
            {
                return RedirectToAction("Index", "Shoping");
            }
            else if (subscribe.Page == "Product")
            {
                return RedirectToAction("Index", "Product");
            }
            else if (subscribe.Page == "ProductDetails")
            {
                return RedirectToAction("Details", "Product", new { id = subscribe.ItemId });
            }
            else if (subscribe.Page == "Blog")
            {
                return RedirectToAction("Index", "Blogs");
            }
            else if (subscribe.Page == "BlogDetails")
            {
                return RedirectToAction("Details", "Blogs", new { id = subscribe.ItemId });
            }
            else if (subscribe.Page == "Faq")
            {
                return RedirectToAction("Index", "Faq");
            }
            else if (subscribe.Page == "Terms")
            {
                return RedirectToAction("Index", "Terms", new { id = subscribe.ItemId });
            }
            else if (subscribe.Page == "Contact")
            {
                return RedirectToAction("Index", "Contact");
            }
            else if (subscribe.Page == "Checkout")
            {
                return RedirectToAction("Index", "Checkout");
            }
            else if (subscribe.Page == "User")
            {
                return RedirectToAction("Index", "User", new { id = subscribe.ItemId });
            }
            else if (subscribe.Page == "Login")
            {
                return RedirectToAction("Login", "User");
            }
            else if (subscribe.Page == "Register")
            {
                return RedirectToAction("Register", "User");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Error()
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
            return View();
        }
    }
}