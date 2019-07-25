using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.Helper;
using Newtonsoft.Json;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Controllers
{
    public class ProductController : Controller
    {
        private readonly ExaxxiDbContext _context;
        CallAPI _api = new CallAPI();

        public ProductController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public IActionResult Index(int? id, int? idBrand, int? idCate, int? idCate_f)
        {
            //Get Department
            if (id == null) ViewBag.Id_Depart = JsonConvert.DeserializeObject<Departments>(_api.getAPI("api/DepartmentsAPI/Take1ByOrder").Result).id; else ViewBag.Id_Depart = id;

            //Get Brand
            if (idBrand == null) ViewBag.Id_Brand = JsonConvert.DeserializeObject<List<Brands>>(_api.getAPI($"api/BrandsAPI/TakeBrandByIdDepart/{ViewBag.Id_Depart}/1").Result).FirstOrDefault().id; else ViewBag.Id_Brand = idBrand;

            //Get Category
            if (idCate == null) ViewBag.Id_Cate = JsonConvert.DeserializeObject<Categories>(_api.getAPI($"api/CategoriesAPI/Take1CateByIdBrand/{ViewBag.Id_Brand}").Result).id; else ViewBag.Id_Cate = idCate;

            //Get Trade_Min
            ViewBag.LA_Min_Min = _api.getAPI("api/ItemsAPI/TakeLowestAskMinMin").Result;

            //Get Trade_Max
            ViewBag.LA_Min_Max = _api.getAPI("api/ItemsAPI/TakeLowestAskMinMax").Result;

            ViewBag.idCate_f = null;
            if (idCate_f != null)
            {
                ViewBag.idCate_f = idCate_f;
                ViewBag.nameCate_f = JsonConvert.DeserializeObject<Categories>(_api.getAPI("api/CategoriesAPI/" + idCate_f).Result).name;
            }

            return View();
        }

        // GET: Product/Details/5
        public IActionResult Details(int? id)
        {
            ViewBag.IdItem = id;
            int idDepart = JsonConvert.DeserializeObject<int>(_api.getAPI("api/ItemsAPI/TakeId_Depart/" + id).Result);
            ViewBag.dsSize = JsonConvert.DeserializeObject<List<ds_Size>>(_api.getAPI("/api/ds_SizeAPI/Takeds_SizeDepart/" + idDepart).Result);
            ViewBag.Sizes = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("/api/SizesAPI/TakeSizesItem/" + id).Result);
            return View();
        }

        public IActionResult Checkout(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IdItem = id;            

            return View();
        }

        public ActionResult Set_SessionCheckout([FromBody] CheckoutViewModel model)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            //Lưu Session
            HttpContext.Session.SetString("ck_account", model.Account);
            HttpContext.Session.SetString("ck_phone", model.Phone);
            HttpContext.Session.SetString("ck_address", model.Address);
            HttpContext.Session.SetString("ck_payment", model.Payment);
            HttpContext.Session.SetInt32("ck_city", model.id_city);
            HttpContext.Session.SetString("ck_shipping", model.shipping);
            HttpContext.Session.SetString("ck_total", model.total.ToString());

            if (model.Enter_bid.ToString() != null)
            {
                HttpContext.Session.SetString("ck_enter_bid", model.Enter_bid.ToString());
            }
            if (model.Exp_Day != null)
            {
                HttpContext.Session.SetString("ck_exp_day", model.Exp_Day);
            }

            return Ok();        
        }

        public IActionResult Last_Checkout(int? id, int act)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IdItem = id;
            ViewBag.Act = act;

            return View();
        }

        public IActionResult Confirm_Checkout(int id, int act, int? size)
        {
            //Buy
            if (act == 0)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }

                var idPost = JsonConvert.DeserializeObject(_api.getAPI("api/ItemsAPI/TakeIdPost_ForOrder/" + id).Result);

                Orders orders = new Orders();

                orders.time = DateTime.Now;
                orders.address = HttpContext.Session.GetString("ck_address").ToString();
                orders.phone = HttpContext.Session.GetString("ck_phone").ToString();
                orders.status = 0;
                orders.id_user = HttpContext.Session.GetInt32("idUser").Value;
                orders.id_post = Convert.ToInt32(idPost);
                orders.service_fee = 0;
                orders.id_city = HttpContext.Session.GetInt32("ck_city").Value;

                if (HttpContext.Session.GetString("ck_payment").ToString() == "Cash On Deliery (COD)")
                {
                    orders.payment = 1;
                }

                if (HttpContext.Session.GetString("ck_payment").ToString() == "Credit / Debit")
                {
                    orders.payment = 2;
                }

                orders.price = 0;
                orders.ship_fee = Convert.ToDouble(HttpContext.Session.GetString("ck_shipping"));
                orders.id_voucher = "1";

                if (_api.postAPI(orders, "api/OrdersChange").Result)
                {
                    return RedirectToAction("Index", "User");
                }
            }

            //Bid
            if (act == 1)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }

                var idSize = JsonConvert.DeserializeObject(_api.getAPI("api/SizesAPI/TakeIdSize_ForBid/" + id +"/" + size).Result);

                Posts posts = new Posts();

                posts.price = Convert.ToDouble(HttpContext.Session.GetString("ck_enter_bid").ToString());
                posts.date_start = DateTime.Now;

                //Xử Lý End Date
                int day = Convert.ToInt16(HttpContext.Session.GetString("ck_exp_day").ToString());
                DateTime now = DateTime.Now;
                DateTime final_day = now.AddDays(day);

                posts.date_end = final_day;

                posts.kind = 2;
                posts.id_size = Convert.ToInt32(idSize);
                posts.id_user = HttpContext.Session.GetInt32("idUser").Value;
                posts.status = 0;
                posts.address = HttpContext.Session.GetString("ck_address").ToString();
                posts.phone = HttpContext.Session.GetString("ck_phone").ToString();
                posts.name_client = HttpContext.Session.GetString("ck_account").ToString();
                posts.id_city = HttpContext.Session.GetInt32("ck_city").Value;

                if (_api.postAPI(posts, "api/PostsChange").Result)
                {
                    return RedirectToAction("Index", "User");
                }
            }

            return View();
        }

        public IActionResult Sell(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IdItem = id;

            return View();
        }

        public ActionResult Set_SessionSell([FromBody] CheckoutViewModel model)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            //Lưu Session
            HttpContext.Session.SetString("sell_account", model.Account);
            HttpContext.Session.SetString("sell_phone", model.Phone);
            HttpContext.Session.SetString("sell_address", model.Address);
            HttpContext.Session.SetString("sell_payment", model.Payment);

            if (model.Enter_ask.ToString() != null)
            {
                HttpContext.Session.SetString("sell_enter_ask", model.Enter_ask.ToString());
            }
            if (model.Exp_Day != null)
            {
                HttpContext.Session.SetString("sell_exp_day", model.Exp_Day);
            }

            return Ok();
        }

        public IActionResult Sell_Confirm(int? id, int act)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IdItem = id;
            ViewBag.Act = act;

            return View();
        }

        public IActionResult Confirm_Sell(int id, int act, int? size)
        {
            //Sell
            if (act == 0)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }

                var idPost = JsonConvert.DeserializeObject(_api.getAPI("api/ItemsAPI/TakeIdPost_ForOrder/" + id).Result);

                Orders orders = new Orders();

                orders.time = DateTime.Now;
                orders.address = HttpContext.Session.GetString("sell_address").ToString();
                orders.phone = HttpContext.Session.GetString("sell_phone").ToString();
                orders.status = 0;
                orders.id_user = HttpContext.Session.GetInt32("idUser").Value;
                orders.id_post = Convert.ToInt32(idPost);
                orders.service_fee = 0;
                orders.address = HttpContext.Session.GetString("sell_address").ToString();
                orders.phone = HttpContext.Session.GetString("sell_phone").ToString();

                if (HttpContext.Session.GetString("sell_payment").ToString() == "Credit / Debit")
                {
                    orders.payment = 2;
                }

                orders.price = 0;
                orders.ship_fee = 0;
                orders.id_voucher = "1";

                if (_api.postAPI(orders, "api/OrdersChange").Result)
                {
                    return RedirectToAction("Index", "User");
                }
            }

            //Ask
            if (act == 1)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }

                var idSize = JsonConvert.DeserializeObject(_api.getAPI("api/SizesAPI/TakeIdSize_ForBid/" + id + "/" + size).Result);

                Posts posts = new Posts();

                posts.price = Convert.ToDouble(HttpContext.Session.GetString("sell_enter_ask").ToString());
                posts.date_start = DateTime.Now;

                //Xử Lý End Date
                int day = Convert.ToInt16(HttpContext.Session.GetString("sell_exp_day").ToString());
                DateTime now = DateTime.Now;
                DateTime final_day = now.AddDays(day);

                posts.date_end = final_day;

                posts.kind = 1;
                posts.id_size = Convert.ToInt32(idSize);
                posts.id_user = HttpContext.Session.GetInt32("idUser").Value;
                posts.status = 0;
                posts.address = HttpContext.Session.GetString("sell_address").ToString();
                posts.phone = HttpContext.Session.GetString("sell_phone").ToString();
                posts.name_client = HttpContext.Session.GetString("sell_account").ToString();
                
                if (_api.postAPI(posts, "api/PostsChange").Result)
                {
                    return RedirectToAction("Index", "User");
                }
            }

            return View();
        }

    }
}
