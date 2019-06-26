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
    [Area("Admin")]
    public class ds_SizeController : Controller
    {
        CallAPI _api = new CallAPI();
       
        // GET: Admin/ds_Size
        public IActionResult Index()
        {
            IEnumerable<ds_Size> result = JsonConvert.DeserializeObject<List<ds_Size>>(_api.getAPI("api/ds_SizeAPI").Result);
            return View(result);
        }

        // GET: Admin/ds_Size/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ds_Size = JsonConvert.DeserializeObject<ds_Size>(_api.getAPI($"api/ds_SizeAPI/Getds_SizeDetail/{id}").Result);
            if (ds_Size == null)
            {
                return NotFound();
            }

            return View(ds_Size);
        }

        // GET: Admin/ds_Size/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ds_Size/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,VN,US,UK,Inch,Centimet")] ds_Size ds_Size)
        {
            if (_api.postAPI(ds_Size, "api/ds_SizeAPI").Result)
            {
                IEnumerable<ds_Size> result = JsonConvert.DeserializeObject<List<ds_Size>>(_api.getAPI("api/ds_SizeAPI").Result);
                return RedirectToAction(nameof(Index));
            }
            return View(ds_Size);
        }

        // GET: Admin/ds_Size/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ds_Size = JsonConvert.DeserializeObject<ds_Size>(_api.getAPI($"api/ds_SizeAPI/{id}").Result);
            if (ds_Size == null)
            {
                return NotFound();
            }
            return View(ds_Size);
        }

        // POST: Admin/ds_Size/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,VN,US,UK,Inch,Centimet")] ds_Size ds_Size)
        {
            if (id != ds_Size.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(ds_Size, $"api/ds_SizeAPI/{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ds_SizeExists(ds_Size.id))
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
            return View(ds_Size);
        }

     
        private bool ds_SizeExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/ds_SizesAPI/ds_SizeExists/{id}").Result);
        }
    }
    
}