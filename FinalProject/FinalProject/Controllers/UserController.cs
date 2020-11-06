using FinalProject.DAL;
using FinalProject.Filters;
using FinalProject.Models;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ShopwiseDB db = new ShopwiseDB();
        [logout]
        public ActionResult Index()
        {
            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                wishlist.RemoveAt(wishlist.Count - 1);
                ViewBag.wishlistCount = wishlist.Count;
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
            ViewBag.Address = db.Address.ToList();
            List<Order> orders = db.Order.ToList();
            return View(orders);
        }

        public ActionResult Login()
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

            return View();
        }

        [HttpPost]
        public ActionResult Login(VMLogin login)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.Include("Addresses").FirstOrDefault(u => u.Username == login.Username);

                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.Password, login.Password))
                    {
                        Session["Loginer"] = user;
                        Session["LoginerId"] = user.Id;

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Password");
                        return View(login);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username");
                    return View(login);
                }
            }

            return View(login);
        }

        public ActionResult Register()
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

            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (db.User.Any(a => a.Email == user.Email))
                {
                    ModelState.AddModelError("", "This email already exists");
                    return View(user);
                }
                if (user.Username==null && user.Password==null)
                {
                    ModelState.AddModelError("", "Username and password is required");
                    return View(user);
                }
                if (db.User.Any(a => a.Username == user.Username))
                {
                    ModelState.AddModelError("", "This username already exists");
                    return View(user);
                }
                User usr = new User();
                usr.Name = user.Name;
                usr.Surname = user.Surname;
                usr.Email = user.Email;
                usr.Username = user.Username;
                usr.Password = Crypto.HashPassword(user.Password);
                usr.CreatedDate = DateTime.Now;
                usr.isRegistered = true;

                db.User.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult ChangePass(VmPass pass)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.FirstOrDefault(u => u.Id == pass.UserId);
                if (user != null)
                {
                    if (Crypto.VerifyHashedPassword(user.Password, pass.oldPass))
                    {
                        if(pass.newPass==pass.confirmPass)
                        {
                            user.Password = Crypto.HashPassword(pass.newPass);
                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            Session["newPass"] = true;
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        Session["oldPass"] = true;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            return View(pass);
        }

        [HttpPost]
        public ActionResult Update(VmUser user)
        {
            User usr = (User)Session["Loginer"];
            //User usr = db.User.Find(user.userId);
            if (usr == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (user.Name != null)
                {
                    usr.Name = user.Name;
                }
                if (user.Surname != null)
                {
                    usr.Surname = user.Surname;
                }
                if (user.Phone != null)
                {
                    usr.Phone = user.Phone;
                }
                if (user.Email != null)
                {
                    usr.Email = user.Email;
                }
                if (user.Username != null)
                {
                    usr.Username = user.Username;
                }
                if (user.ImageFile != null)
                {
                    if (usr.Image != null)
                    {
                        string oldImagePath = Path.Combine(Server.MapPath("~/Uploads/User/"), usr.Image);
                        System.IO.File.Delete(oldImagePath);

                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + user.ImageFile.FileName;
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads/User/"), imageName);

                        user.ImageFile.SaveAs(imagePath);
                        usr.Image = imageName;
                    }
                    else
                    {
                        string imageName = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + user.ImageFile.FileName;
                        string imagePath = Path.Combine(Server.MapPath("~/Uploads/User/"), imageName);

                        user.ImageFile.SaveAs(imagePath);
                        usr.Image = imageName;
                    }
                }
                db.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Address(VmAddress address)
        {
            User user = db.User.Find(address.userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (user.Addresses == null)
                {
                    Address adrs = new Address();
                    adrs.City = address.City;
                    adrs.Country = address.Country;
                    adrs.Zipcode = address.Zipcode;
                    adrs.Street = address.Street;
                    adrs.DetailedAddress = address.DetailedAddress;
                    adrs.UserId = user.Id;
                    if (address.isPrimary == "true")
                    {
                        adrs.IsPrimary = true;
                    }
                    else if(address.isPrimary == "false")
                    {
                        adrs.IsPrimary = false;
                    }
                    db.Address.Add(adrs);
                    db.SaveChanges();
                }
                else
                {
                    Address adrs = db.Address.FirstOrDefault(u => u.UserId == address.userId);
                    adrs.City = address.City;
                    adrs.Country = address.Country;
                    adrs.Zipcode = address.Zipcode;
                    adrs.Street = address.Street;
                    adrs.DetailedAddress = address.DetailedAddress;
                    db.Entry(adrs).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            return View(address);
        }

        public ActionResult RecentlyView()
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

            VmProduct model = new VmProduct();
            List<string> ids = new List<string>();
            if (Request.Cookies["SeenProId"] != null)
            {
                ids.AddRange(Request.Cookies["SeenProId"].Value.Split(','));
            }
            model.SeenPro = db.Product.Include("productInfos").Include("productInfos.Images").Include("productInfos.SaleBanner")
                .Where(i => ids.Contains(i.Id.ToString())).ToList();
            model.Ids = ids;
            return View(model);
        }
                
        [logout]
        public ActionResult Oreders()
        {
            int userId = (int)Session["LoginerId"];

            #region Wish List and Cart
            HttpCookie cookieList = Request.Cookies["WishList"];
            HttpCookie cookieCart = Request.Cookies["Cart"];
            if (cookieList != null)
            {
                List<string> wishlist = Request.Cookies["WishList"].Value.Split(',').ToList();
                wishlist.RemoveAt(wishlist.Count - 1);
                ViewBag.wishlistCount = wishlist.Count;
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

            List<OrderItem> items = db.OrderItem.Include("ProductInfo")
                                                .Include("ProductInfo.Images")
                                                .Include("ProductInfo.Product")
                                                .Include("Order")
                                                .Where(s => s.Order.UserId == userId).ToList();
            return View(items);
        }

        public ActionResult DeleteUser(VmUser user)
        {
            User usr = db.User.Find(user.userId);
            if (usr == null)
            {
                return HttpNotFound();
            }
            if (usr.ImageFile != null)
            {
                string imgPath = Path.Combine(Server.MapPath("~/Uploads/Partner/"), usr.Image);
                System.IO.File.Delete(imgPath);
            }
            db.User.Remove(usr);
            db.SaveChanges();
            return RedirectToAction("Login", "User");
        }
    }
}