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
    public class BrandsController : Controller
    {
        CallAPI _api = new CallAPI();
        // GET: Admin/Brands
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Brands> result = JsonConvert.DeserializeObject<List<Brands>>(_api.getAPI("api/BrandsAPI/BrandsDefault", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Brands/Details/5
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

            var brands = JsonConvert.DeserializeObject<Brands>(_api.getAPI($"api/BrandsAPI/GetbrandsDetail/{id}", HttpContext.Session.GetString("token")).Result);
            if (brands == null)
            {
                return NotFound();
            }

            return View(brands);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile img, [Bind("id,name,img,active,order,id_department")] Brands brands)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            //Nhận file POST qua
            if (img == null || img.Length == 0)
                return Content("Không File nào được chọn!");

            //Save File da upload vao thu muc MyFiles
            string fullname = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot", "images", "brand", img.FileName);

            using (var myfile = new FileStream(fullname, FileMode.Create))
            {
                img.CopyTo(myfile);
            }

            //Gán tên file vào img để lưu vào DB
            brands.img = img.FileName;

            if (_api.postAPI(brands, "api/BrandsChange", HttpContext.Session.GetString("token")).Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(brands);
        }

        // GET: Admin/Brands/Edit/5
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

            var brands = JsonConvert.DeserializeObject<Brands>(_api.getAPI($"api/BrandsAPI/{id}", HttpContext.Session.GetString("token")).Result);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,name,img,active,order,id_department")] Brands brands, IFormFile img)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != brands.id)
            {
                return NotFound();
            }


            try
            {
                //Nhận file POST qua
                if (img == null || img.Length == 0)
                {
                    return Content("Không File nào được chọn!");

                }


                //Save File da upload vao thu muc MyFiles
                string fullname = Path.Combine
                    (Directory.GetCurrentDirectory(), "wwwroot", "images", "brand", img.FileName);

                using (var myfile = new FileStream(fullname, FileMode.Create))
                {
                    await img.CopyToAsync(myfile);
                }
                brands.img = img.FileName;
                var result = await _api.putAPI(brands, $"api/BrandsChange/{id}", HttpContext.Session.GetString("token"));
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


        private bool BrandsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/BrandsAPI/BrandsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
