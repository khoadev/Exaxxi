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

namespace Exaxxi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BrandsController : Controller
    {
        CallAPI _api = new CallAPI();

       

        // GET: Admin/Brands
        public IActionResult Index()
        {
            IEnumerable<Brands> result = JsonConvert.DeserializeObject<List<Brands>>(_api.getAPI("api/BrandsAPI").Result);
            return View(result);
        }

        // GET: Admin/Brands/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = JsonConvert.DeserializeObject<Brands>(_api.getAPI($"api/BrandsAPI/GetbrandsDetail/{id}").Result);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,name,active,order,id_department")] Brands brands)
        {
            if (_api.postAPI(brands, "api/BrandsAPI").Result)
            {
                IEnumerable<Brands> result = JsonConvert.DeserializeObject<List<Brands>>(_api.getAPI("api/BrandsAPI").Result);
                return RedirectToAction(nameof(Index));
            }
       
            return View(brands);
        }

        // GET: Admin/Brands/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brands = JsonConvert.DeserializeObject<Brands>(_api.getAPI($"api/BrandsAPI/{id}").Result);
            if (brands == null)
            {
                return NotFound();
            }
            return View(brands);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,active,order,id_department")] Brands brands)
        {
            if (id != brands.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(brands, $"api/BrandsAPI/{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandsExists(brands.id))
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
           
            return View(brands);
        }

        
        private bool BrandsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/BrandsAPI/BrandsExists/{id}").Result);
        }
    }
}
