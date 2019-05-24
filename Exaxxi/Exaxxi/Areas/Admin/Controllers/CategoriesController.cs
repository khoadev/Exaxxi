﻿using System;
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
    public class CategoriesController : Controller
    {
        
        CallAPI _api = new CallAPI();
        

        // GET: Admin/Categories
        public IActionResult Index()
        {
            IEnumerable<Categories> result = JsonConvert.DeserializeObject<List<Categories>>(_api.getAPI("api/CategoriesAPI").Result);
            return View(result);
        }

        // GET: Admin/Categories/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categories = JsonConvert.DeserializeObject<Categories>(_api.getAPI($"api/CategoriesAPI/GetCategoriesDetail/{id}").Result);
            
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,vi_name,active,order,id_brand")] Categories categories)
        {
            if (_api.postAPI(categories, "api/CategoriesAPI").Result)
            {
                IEnumerable<Categories> result = JsonConvert.DeserializeObject<List<Categories>>(_api.getAPI("api/CategoriesAPI").Result);
                ViewData["id_brand"] = result;
                return RedirectToAction(nameof(Index));
                
            }
            
            return View(categories);
        }

        // GET: Admin/Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = JsonConvert.DeserializeObject<Categories>(_api.getAPI($"api/CategoriesAPI/{id}").Result);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,vi_name,active,order,id_brand")] Categories categories)
        {
            if (id != categories.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(categories, $"api/CategoriesAPI/{id}");
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
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/CategoriesAPI/CategoriesExists/{id}").Result);
        }
    }
}