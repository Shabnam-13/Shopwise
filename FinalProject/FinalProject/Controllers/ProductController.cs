using FinalProject.DAL;
using FinalProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
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

            VmProduct model = new VmProduct();
            model.Products = db.Product.Include("productInfos")
                                       .Include("productInfos.Images")
                                       .Include("productInfos.SaleBanner")
                                       .Where(p=>p.productInfos.Count!=0)
                                       .ToList();
            model.ProductInfos = db.ProductInfos.ToList();
            model.Images = db.ProductImages.ToList();

            return View(model);
        }

        public ActionResult Details(int? id)
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

            #region Cookie
            List<string> ids = new List<string>();
            HttpCookie cookie = new HttpCookie("SeenProId");
            if (Request.Cookies["SeenProId"] != null)
            {
                ids.AddRange(Request.Cookies["SeenProId"].Value.Split(','));
                if (!ids.Any(i => i.Contains(id.ToString())))
                {
                    ids.Insert(0, id.ToString());
                }
                cookie.Value = string.Join(",", ids);
            }
            else
            {
                cookie.Value = id.ToString();
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            #endregion
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

            if (id != null)
            {
                VmProduct model = new VmProduct();
                model.product = db.Product.Find(id);

                model.Products = db.Product.Include("productInfos")
                                           .Include("productInfos.Images")
                                           .Include("productInfos.SaleBanner")
                                           .Where(p => p.productInfos.Count != 0)
                                           .ToList();
                model.Colors = db.Colors.Where(p => p.Products.FirstOrDefault().ProductId == id).ToList();
                model.Sizes = db.Size.Where(p => p.Products.FirstOrDefault().ProductId == id).ToList();
                model.categories = db.Category.ToList();
                model.subcategories = db.Subcategory.ToList();
                model.SubOfSubcategories = db.SubOfSubcategory.ToList();
                model.subOfsubcat = db.SubOfSubcategory.FirstOrDefault(s=>s.proCategories.FirstOrDefault().ProductId==id);
                model.ProductInfos = db.ProductInfos.Include("Images")
                                                    .Include("SaleBanner")
                                                    .Include("Size")
                                                    .Include("Color")
                                                    .Include("sizeOptions")
                                                    .Include("Product")
                                                    .Where(p => p.ProductId == id).ToList();
                model.productInfo = db.ProductInfos.Include("Product").FirstOrDefault(p => p.ProductId == id);
                model.sizeOptions = db.SizeOption.Where(p => p.productInfoId == model.productInfo.Id).ToList();

                List<string> Ids = new List<string>();
                if (Request.Cookies["SeenProId"] != null)
                {
                    Ids.AddRange(Request.Cookies["SeenProId"].Value.Split(','));
                }
                model.SeenPro = db.Product.Include("productInfos")
                                          .Include("productInfos.Images")
                                          .Include("productInfos.SaleBanner")
                                          .Where(i => Ids.Contains(i.Id.ToString())).ToList();
                model.Ids = Ids;
                if (model.product == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                return View(model);
            }
            return RedirectToAction("Error", "Home");
        }

        public JsonResult Wishlist(int? id)
        {
            string response = "";
            if (id != null)
            {
                if (Request.Cookies["WishList"] != null)
                {
                    string oldList = Request.Cookies["WishList"].Value;
                    HttpCookie cookie = new HttpCookie("WishList");
                    cookie.Value = oldList;

                    if (!oldList.Contains(id.ToString()))
                    {
                        cookie.Value += id + ",";
                        Request.Cookies["WishList"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "successTrue";
                    }
                    else
                    {
                        List<string> oldListArr = oldList.Split(',').ToList();
                        oldListArr.Remove(id.ToString());

                        oldList = string.Join(",", oldListArr);
                        cookie.Value = oldList;
                        Request.Cookies["WishList"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "successFalse";
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("WishList");

                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value += id + ",";
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

        public JsonResult ShoppingCart(int? id, decimal? count)
        {
            string response = "";
            if (id != null && count != null)
            {
                if (Request.Cookies["Cart"] != null)
                {
                    string oldList = Request.Cookies["Cart"].Value;
                    HttpCookie cookie = new HttpCookie("Cart");
                    cookie.Value = oldList;
                    List<string> cartList = oldList.Split(',').ToList();
                    cartList.RemoveAt(cartList.Count - 1);

                    string cartElement = cartList.FirstOrDefault(c => Convert.ToInt32(c.Split('-')[0]) == id);

                    if (cartElement == null)
                    {
                        cookie.Value += id + "-" + count + ",";
                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "succesTrue";
                    }
                    else
                    {
                        cartList.Remove(cartElement);

                        if (cartList.Count() > 0)
                        {
                            cookie.Value = string.Join(",", cartList) + ",";
                            cookie.Value += id + "-" + count + ",";
                        }
                        else
                        {
                            cookie.Value = id + "-" + count + ",";
                        }

                        Request.Cookies["Cart"].Expires = DateTime.Now.AddYears(1);
                        Response.Cookies.Add(cookie);
                        response = "successFalse";
                    }
                }
                else
                {
                    HttpCookie cookie = new HttpCookie("Cart");

                    cookie.Expires = DateTime.Now.AddYears(1);
                    cookie.Value += id + "-" + count + ",";
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

        [HttpPost]
        public ActionResult Review()
        {
            return View();
        }

        public ActionResult Category(int? id)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

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
            if (id != null)
            {
                VmProduct pro = new VmProduct();
                pro.category = db.Category.Find(id);
                if (pro.category == null)
                {
                    return RedirectToAction("Error", "Home");

                }
                pro.Products = db.Product.Include("productInfos")
                                         .Include("productInfos.Images")
                                         .Include("productInfos.SaleBanner")
                                         .Where(c => c.ProCategories.FirstOrDefault().SubOfSubcategory.Subcategory.CategoryId == id)
                                         .ToList();
                pro.subcategories = db.Subcategory.Where(s => s.CategoryId == id).ToList();

                return View(pro);
            }
            else
            {
                return RedirectToAction("Error", "Home");

            }

        }
        public ActionResult Subcategory(int? id)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

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

            if (id != null)
            {
                VmProduct pro = new VmProduct();
                pro.subcat = db.Subcategory.Find(id);
                if (pro.subcat == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                pro.Products = db.Product.Include("productInfos")
                                         .Include("productInfos.Images")
                                         .Include("productInfos.SaleBanner")
                                         .Where(c => c.ProCategories.FirstOrDefault().SubOfSubcategory.SubcategoryId == id)
                                         .ToList();
                pro.SubOfSubcategories = db.SubOfSubcategory.Where(s => s.SubcategoryId == id).ToList();

                return View(pro);
            }
            else
            {
                return RedirectToAction("Error", "Home");

            }

        }
        public ActionResult SubOfSubcategory(int? id)
        {
            ViewBag.Category = db.Category.Include("Subcategories").Where(s => s.Subcategories.Count != 0).ToList();

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
            if (id != null)
            {
                VmProduct pro = new VmProduct();
                pro.subOfsubcat = db.SubOfSubcategory.Find(id);
                if (pro.subOfsubcat == null)
                {
                    return RedirectToAction("Error", "Home");

                }
                pro.Products = db.Product.Include("productInfos")
                                         .Include("productInfos.Images")
                                         .Include("productInfos.SaleBanner")
                                         .Where(c => c.ProCategories.FirstOrDefault().SubOfSubcategoryId == id)
                                         .ToList();
                return View(pro);
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }

        }
    }
}