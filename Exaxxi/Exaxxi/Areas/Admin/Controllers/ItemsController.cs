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
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoMapper;
using PagedList.Core;
using Exaxxi.Common;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ItemsController : Controller
    {
        CallAPI _api = new CallAPI();
        private readonly IMapper mapper;
        private IHostingEnvironment hostingEnvironment;
        public ItemsController(IMapper _map, IHostingEnvironment hostingEnvironment)
        {
            mapper = _map;
            this.hostingEnvironment = hostingEnvironment;
        }
        // GET: Admin/Items
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
           
            return View();
        }

        // GET: Admin/Items/Details/5
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

            var items = JsonConvert.DeserializeObject<ItemViewAdmin>(_api.getAPI($"api/ItemsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Admin/Items/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,vi_info,en_info,img,img3d,volatility,trade_min,trade_max,active,lowest_ask,highest_bid,sold,id_admin,id_category")] Items items, IFormFile img, List<IFormFile> img3d)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            //tao thu muc
            var path = Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads");
            string pathString = System.IO.Path.Combine(path, items.name);
            System.IO.Directory.CreateDirectory(pathString);
            pathString = System.IO.Path.Combine(pathString, items.name);
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {


                }

            }


            //Nhận file POST qua
            if (img == null || img.Length == 0)
                return Content("Không File nào được chọn!");

            //Save File da upload vao thu muc MyFiles
            string fullname = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot", "images", "item", img.FileName);

            using (var myfile = new FileStream(fullname, FileMode.Create))
            {
                await img.CopyToAsync(myfile);
            }
            // hinh 3d
            foreach (var file3d in img3d)
            {
                string fullname3d = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot", "uploads", items.name, file3d.FileName);

                using (var myfile3d = new FileStream(fullname3d, FileMode.Create))
                {
                    await img.CopyToAsync(myfile3d);
                }
                items.img3d = file3d.FileName;
            }
            items.folder = items.name;
            //Gán tên file vào img để lưu vào DB

            
            items.img = img.FileName;
            //Post sang API xử lý
            if (_api.postAPI(items, "api/ItemsChange", HttpContext.Session.GetString("token")).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(items);
        }

        // GET: Admin/Items/Edit/5
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

            var items = JsonConvert.DeserializeObject<Items>(_api.getAPI($"api/ItemsAPI/GetItemsEdit/{id}", HttpContext.Session.GetString("token")).Result);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Admin/Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,vi_info,en_info,img,volatility,trade_min,trade_max,active,lowest_ask,highest_bid,sold,id_admin,id_category")] Items items, IFormFile img)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != items.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Nhận file POST qua
                    if (img == null || img.Length == 0)
                    {
                        return Content("Không File nào được chọn!");

                    }


                    //Save File da upload vao thu muc MyFiles
                    string fullname = Path.Combine
                        (Directory.GetCurrentDirectory(), "wwwroot", "images", "item", img.FileName);

                    using (var myfile = new FileStream(fullname, FileMode.Create))
                    {
                        await img.CopyToAsync(myfile);
                    }
                    items.img = img.FileName;
                    var result = await _api.putAPI(items, $"api/ItemsChange/{id}", HttpContext.Session.GetString("token"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.id))
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
            return View(items);
        }


        private bool ItemsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/ItemsAPI/ItemsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
