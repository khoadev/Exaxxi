using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Exaxxi.Helper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Exaxxi.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class CategoriesController : Controller
    {

        CallAPI _api = new CallAPI();


        // GET: Admin/Categories
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Categories> result = JsonConvert.DeserializeObject<List<Categories>>(_api.getAPI("api/CategoriesAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return NotFound();
            }
            var categories = JsonConvert.DeserializeObject<Categories>(_api.getAPI($"api/CategoriesAPI/GetCategoriesDetail/{id}", HttpContext.Session.GetString("token")).Result);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,name,active,order,id_brand")] Categories categories)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (_api.postAPI(categories, "api/CategoriesChange", HttpContext.Session.GetString("token")).Result)
            {
                IEnumerable<Categories> result = JsonConvert.DeserializeObject<List<Categories>>(_api.getAPI("api/CategoriesAPI", HttpContext.Session.GetString("token")).Result);
                return RedirectToAction(nameof(Index));

            }

            return View(categories);
        }

        // GET: Admin/Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var categories = JsonConvert.DeserializeObject<Categories>(_api.getAPI($"api/CategoriesAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (categories == null)
            {
                return NotFound();
            }


            return View(categories);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,active,order,id_brand")] Categories categories)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != categories.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(categories, $"api/CategoriesChange/{id}", HttpContext.Session.GetString("token"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(categories);
        }

        private bool CategoriesExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/CategoriesAPI/CategoriesExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
