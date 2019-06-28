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
            if (idBrand == null) ViewBag.Id_Brand = JsonConvert.DeserializeObject<Brands>(_api.getAPI($"api/BrandsAPI/Take1BrandByIdDepart/{ViewBag.Id_Depart}").Result).id; else ViewBag.Id_Brand = idBrand;

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

            return View();
        }

        public IActionResult Checkout(int? id)
        {
            ViewBag.IdItem = id;

            return View();
        }
    }
}
