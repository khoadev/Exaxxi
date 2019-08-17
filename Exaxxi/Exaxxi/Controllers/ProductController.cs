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
using Exaxxi.Common;

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
        public IActionResult Details(int? id, int? size)
        {
            ViewBag.IdItem = id;
            int idDepart = JsonConvert.DeserializeObject<int>(_api.getAPI("api/ItemsAPI/TakeId_Depart/" + id).Result);
            ViewBag.dsSize = JsonConvert.DeserializeObject<List<ds_Size>>(_api.getAPI("/api/ds_SizeAPI/Takeds_SizeDepart/" + idDepart).Result);
            ViewBag.Sizes = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("/api/SizesAPI/TakeSizesItem/" + id).Result);
            if (size == null)
            {
                ViewBag.SizeVN = _api.getAPI("/api/ds_SizeAPI/TakeFirst/" + id).Result;
            }
            else
            {
                ViewBag.SizeVN = _api.getAPI("/api/ds_SizeAPI/TakeSize/" + id + "/" + size).Result;
            }
            return View();
        }

        public IActionResult Checkout(int? id, int size)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.SizeVN = size;

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
            HttpContext.Session.SetInt32("ck_id_voucher", Convert.ToInt32(model.id_voucher));
            HttpContext.Session.SetString("ck_shipping", model.shipping);
            HttpContext.Session.SetString("ck_total", model.total.ToString());
            HttpContext.Session.SetString("ck_discount", model.discount.ToString());

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

        public IActionResult Last_Checkout(int? id, int act, int size)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.SizeVN = size;
            ViewBag.IdItem = id;
            ViewBag.Act = act;

            return View();
        }

        public async Task<IActionResult> Confirm_CheckoutAsync(int id, int act, int? size)
        {
            //Buy
            if (act == 0)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }
                //var i = HttpContext.Session.GetString("ck_discount");
                var idPost = JsonConvert.DeserializeObject(_api.getAPI("api/SizesAPI/TakeIdPost_ForOrder/" + id + "/" + size + "/1").Result);
                
                Orders orders = new Orders();

                orders.time = DateTime.Now;
                orders.address = HttpContext.Session.GetString("ck_address").ToString();
                orders.phone = HttpContext.Session.GetString("ck_phone").ToString();
                orders.status = 0;
                orders.id_user = HttpContext.Session.GetInt32("idUser").Value;
                orders.id_post = Convert.ToInt32(idPost);
                orders.service_fee = 0;
                orders.id_city = HttpContext.Session.GetInt32("ck_city").Value;
                if (HttpContext.Session.GetString("ck_discount") != "")
                {
                    orders.discount = Convert.ToDouble(HttpContext.Session.GetString("ck_discount"));
                }
               

                if (HttpContext.Session.GetString("ck_payment").ToString() == "Cash On Deliery (COD)")
                {
                    orders.payment = 1;
                }
                else
                if (HttpContext.Session.GetString("ck_payment").ToString() == "Credit / Debit")
                {
                    orders.payment = 2;
                }

                orders.price = Convert.ToDouble(HttpContext.Session.GetString("ck_total"));
                orders.ship_fee = Convert.ToDouble(HttpContext.Session.GetString("ck_shipping"));
                if (HttpContext.Session.GetInt32("ck_id_voucher") != null)
                {
                    orders.id_voucher = HttpContext.Session.GetInt32("ck_id_voucher").Value;
                    await _api.getAPI("api/VouchersAPI/UpdateCount/" + HttpContext.Session.GetInt32("ck_id_voucher").Value);
                }

                //Send Mail 
                var email_post = _api.getAPI("api/PostsAPI/SelectEmailUser/" + idPost).Result;
                Mailer ml = new Mailer();                
                ml.SendMail("Exaxxi Site", email_post, "Buy Product", "Your buying has something new. Please check details in your account!");

                if (_api.postAPI(orders, "api/OrdersChange").Result)
                {
                    //update after buy
                    var post = JsonConvert.DeserializeObject<Posts>(_api.getAPI("api/PostsAPI/" + idPost).Result);
                    post.status = 1;
                    await _api.putAPI(post, "api/PostsChange/" + idPost);
                    var id_size = JsonConvert.DeserializeObject<int>(_api.getAPI("api/SizesAPI/TakeIdSize_ForBid/" + id + "/" + size).Result);
                    UpdatePrice(id, id_size);
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

        public IActionResult Sell(int? id, int size)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.SizeVN = size;
            ViewBag.level_seller = 0;
            ViewBag.IdItem = id;
            ViewBag.payment_fee = Exaxxi.Common.info.payment_fee;

            //CheckSession
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("sell_enter_ask")))
            {
                HttpContext.Session.Remove("sell_enter_ask");
            }

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
            HttpContext.Session.SetString("sell_payment_fee", model.Payment_fee.ToString());
            HttpContext.Session.SetString("sell_service_fee", model.Service_fee.ToString());
            HttpContext.Session.SetString("sell_total_price", model.Total_price.ToString());
            HttpContext.Session.SetString("sell_id_city", model.id_city.ToString());

            if (model.Enter_ask != 0)
            {
                HttpContext.Session.SetString("sell_enter_ask", model.Enter_ask.ToString());
            }
            if (model.Exp_Day != null)
            {
                HttpContext.Session.SetString("sell_exp_day", model.Exp_Day);
            }

            return Ok();
        }

        public IActionResult Sell_Confirm(int? id, int act, int? size)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.SizeVN = size;
            ViewBag.IdItem = id;
            ViewBag.Act = act;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("sell_enter_ask")))
            {
                ViewBag.PriceTemp = HttpContext.Session.GetString("sell_enter_ask");
            }
            

            return View();
        }

        public IActionResult Confirm_Sell(int id, int act, int size, int idPost = 0)
        {
            //Sell
            if (act == 0)
            {
                if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
                {
                    return RedirectToAction("Index", "Login");
                }
                
                if(idPost == 0) idPost = JsonConvert.DeserializeObject<int>(_api.getAPI("api/SizesAPI/TakeIdPost_ForOrder/" + id + "/" + size +"/2").Result);

                Orders orders = new Orders();

                orders.time = DateTime.Now;
                orders.address = HttpContext.Session.GetString("sell_address").ToString();
                orders.phone = HttpContext.Session.GetString("sell_phone").ToString();
                orders.status = 0;
                orders.id_user = HttpContext.Session.GetInt32("idUser").Value;
                orders.id_post = Convert.ToInt32(idPost);
                orders.service_fee = Convert.ToDouble(HttpContext.Session.GetString("sell_service_fee"));
                orders.payment_pro = Convert.ToDouble(HttpContext.Session.GetString("sell_payment_fee"));
                orders.price = Convert.ToDouble(HttpContext.Session.GetString("sell_total_price"));
                orders.id_city = Convert.ToInt32(HttpContext.Session.GetString("sell_id_city"));

                if (HttpContext.Session.GetString("sell_payment").ToString() == "Credit / Debit")
                {
                    orders.payment = 2;
                }
                else
                if (HttpContext.Session.GetString("sell_payment").ToString() == "Cash On Deliery (COD)")
                {
                    orders.payment = 1;
                }

                //Send Mail 
                var email_post = _api.getAPI("api/PostsAPI/SelectEmailUser/" + idPost).Result;
                Mailer ml = new Mailer();
                ml.SendMail("Exaxxi Site", email_post, "Sell Product", "Your selling has something new. Please check details in your account!");
                
                if (_api.postAPI(orders, "api/OrdersChange").Result)
                {
                    //update after sell
                    var post = JsonConvert.DeserializeObject<Posts>(_api.getAPI("api/PostsAPI/" + idPost).Result);
                    post.status = 1;
                    _api.putAPI(post, "api/PostsChange/" + idPost);
                    var id_size = JsonConvert.DeserializeObject<int>(_api.getAPI("api/SizesAPI/TakeIdSize_ForBid/" + id + "/" + size).Result);
                    UpdatePrice(id, id_size);

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
                double price = Convert.ToDouble(HttpContext.Session.GetString("sell_enter_ask").ToString());
                var idSize = JsonConvert.DeserializeObject(_api.getAPI("api/SizesAPI/TakeIdSize_ForBid/" + id + "/" + size).Result);
                Posts postmatch = JsonConvert.DeserializeObject<Posts>(_api.getAPI("api/PostsAPI/FindPostMatchForAsk/" + idSize + "/" + price).Result);
                if (postmatch != null)
                {
                    return Confirm_Sell(id,0,size,postmatch.id);
                }
                else
                {
                    Posts posts = new Posts();

                    posts.price = price;
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
                    posts.id_city = Convert.ToInt32(HttpContext.Session.GetString("sell_id_city"));
                    if (_api.postAPI(posts, "api/PostsChange").Result)
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
            }

            return View();
        }
        public async void UpdatePrice(int id_item, int id_size)
        {
            var size = JsonConvert.DeserializeObject<Sizes>(_api.getAPI("api/SizesAPI/" + id_size).Result);
            var item = JsonConvert.DeserializeObject<Items>(_api.getAPI("api/ItemsAPI/GetItemsEdit/" + id_item).Result);
            UpdatePrice price1 = new UpdatePrice() { id = size.id, lowest_ask = size.lowest_ask, highest_bid = size.highest_bid };

            double lowestAskSizeUp = JsonConvert.DeserializeObject<double>(_api.getAPI("api/PostsAPI/TakeLowestAsk/" + id_item + "/" + id_size).Result);
            double highestBidSizeUp = JsonConvert.DeserializeObject<double>(_api.getAPI("api/PostsAPI/TakeHighestBid/" + id_item + "/" + id_size).Result);
            if (size.lowest_ask < lowestAskSizeUp || size.highest_bid > highestBidSizeUp || lowestAskSizeUp == 0)
            {
                if (size.lowest_ask < lowestAskSizeUp || lowestAskSizeUp == 0) price1.lowest_ask = lowestAskSizeUp;
                if (size.highest_bid > highestBidSizeUp) price1.highest_bid = highestBidSizeUp;
                await _api.putAPI(price1, "api/SizesChange/UpdatePrice");
            }

            UpdatePrice price2 = new UpdatePrice() { id = item.id, lowest_ask = item.lowest_ask.Value, highest_bid = item.highest_bid.Value };

            double lowestAskUp = JsonConvert.DeserializeObject<double>(_api.getAPI("api/PostsAPI/TakeLowestAsk/" + id_item + "/0").Result);
            double highestBidUp = JsonConvert.DeserializeObject<double>(_api.getAPI("api/PostsAPI/TakeHighestBid/" + id_item + "/0").Result);
            if (item.lowest_ask != lowestAskUp || item.highest_bid != highestBidUp)
            {
                if (item.lowest_ask < lowestAskUp || lowestAskUp == 0) price2.lowest_ask = lowestAskUp;
                if (item.highest_bid > highestBidUp) price2.highest_bid = highestBidUp;
                await _api.putAPI(price2, "api/ItemsChange/UpdatePrice");
            }
        }
    }
}
