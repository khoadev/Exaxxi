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
    
    [Area("Admin")]
    public class SizesController : Controller
    {
        CallAPI _api = new CallAPI();
        
        // GET: Admin/Sizes
        public IActionResult Index()
        {
            IEnumerable<Sizes> result = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("api/SizesAPI").Result);
            return View(result);
        }

        // GET: Admin/Sizes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizes = JsonConvert.DeserializeObject<Sizes>(_api.getAPI($"api/SizesAPI/GetSizesDetail/{id}").Result);
            if (sizes == null)
            {
                return NotFound();
            }

            return View(sizes);
        }

        // GET: Admin/Sizes/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Admin/Sizes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,lowest_ask,highest_bid,last_sale,id_ds_size,id_item")] Sizes sizes)
        {
            if (_api.postAPI(sizes, "api/SizesAPI").Result)
            {
                IEnumerable<Sizes> result = JsonConvert.DeserializeObject<List<Sizes>>(_api.getAPI("api/SizesAPI").Result);
                return RedirectToAction(nameof(Index));
            }

         
            return View(sizes);
        }

        // GET: Admin/Sizes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sizes = JsonConvert.DeserializeObject<Sizes>(_api.getAPI($"api/SizesAPI/{id}").Result);
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
            if (id != sizes.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(sizes, $"api/SizesAPI/{id}");
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
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/SizesAPI/SizesExists/{id}").Result);
        }
    }
}
