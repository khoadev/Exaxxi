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
    public class SizesController : Controller
    {
        CallAPI _api = new CallAPI();

        // GET: Admin/Sizes
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Sizes> result = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("api/SizesAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Sizes/Details/5
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

            var sizes = JsonConvert.DeserializeObject<Sizes>(_api.getAPI($"api/SizesAPI/GetSizesDetail/{id}", HttpContext.Session.GetString("token")).Result);
            if (sizes == null)
            {
                return NotFound();
            }

            return View(sizes);
        }

        // GET: Admin/Sizes/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,lowest_ask,highest_bid,last_sale,id_ds_size,id_item")] Sizes sizes)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (_api.postAPI(sizes, "api/SizesChange", HttpContext.Session.GetString("token")).Result)
            {
                IEnumerable<Sizes> result = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("api/SizesAPI", HttpContext.Session.GetString("token")).Result);
                return RedirectToAction(nameof(Index));
            }


            return View(sizes);
        }

        // GET: Admin/Sizes/Edit/5
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

            var sizes = JsonConvert.DeserializeObject<Sizes>(_api.getAPI($"api/SizesAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (sizes == null)
            {
                return NotFound();
            }
            return View(sizes);
        }

        // POST: Admin/Sizes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,lowest_ask,highest_bid,last_sale,id_ds_size,id_item")] Sizes sizes)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != sizes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(sizes, $"api/SizesChange/{id}", HttpContext.Session.GetString("token"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizesExists(sizes.id))
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

            return View(sizes);
        }


        private bool SizesExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/SizesAPI/SizesExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
