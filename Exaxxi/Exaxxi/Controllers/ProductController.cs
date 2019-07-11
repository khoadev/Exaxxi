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
            int idDepart = JsonConvert.DeserializeObject<Items>(_api.getAPI("api/ItemsAPI/" + id).Result).category.brand.id_department;
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

        public IActionResult Confirm_Checkout(int id)
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

            if (_api.postAPI(orders, "api/OrdersChange").Result)
            {
                return RedirectToAction("Index","User");
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
    }
}
