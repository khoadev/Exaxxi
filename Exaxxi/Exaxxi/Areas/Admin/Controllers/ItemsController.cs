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
using System.IO;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
  
    public class ItemsController : Controller
    {
        CallAPI _api = new CallAPI();
        // GET: Admin/Items
        public IActionResult Index()
        {
            IEnumerable<Items> result = JsonConvert.DeserializeObject<List<Items>>(_api.getAPI("api/ItemsAPI").Result);
            return View(result);
        }

        // GET: Admin/Items/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = JsonConvert.DeserializeObject<Items>(_api.getAPI($"api/ItemsAPI/GetItemsDetail/{id}").Result);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Admin/Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,vi_info,en_info,img,volatility,trade_min,trade_max,active,lowest_ask,highest_bid,sold,id_admin,id_category")] Items items, IFormFile img)
        {
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

            //Gán tên file vào img để lưu vào DB
            items.img = img.FileName;
            //Post sang API xử lý
            if (_api.postAPI(items, "api/ItemsAPI").Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(items);
        }

        // GET: Admin/Items/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = JsonConvert.DeserializeObject<Items>(_api.getAPI($"api/ItemsAPI/{id}").Result);
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
                    var result = await _api.putAPI(items, $"api/ItemsAPI/{id}");
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
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/ItemsAPI/ItemsExists/{id}").Result);
        }
    }
}
