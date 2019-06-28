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
            //var items = JsonConvert.DeserializeObject<Items>(_api.getAPI($"api/ItemsAPI/{id}").Result);

            //if (items == null)
            //{
            //    return NotFound();
            //}

            ViewBag.IdItem = id;

            return View(/*items*/);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["id_admin"] = new SelectList(_context.Admins, "id", "email");
            ViewData["id_category"] = new SelectList(_context.Categories, "id", "name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,vi_info,en_info,img,volatility,trade_min,trade_max,active,lowest_ask,highest_bid,sold,id_admin,id_category")] Items items)
        {
            if (ModelState.IsValid)
            {
                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_admin"] = new SelectList(_context.Admins, "id", "email", items.id_admin);
            ViewData["id_category"] = new SelectList(_context.Categories, "id", "name", items.id_category);
            return View(items);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            ViewData["id_admin"] = new SelectList(_context.Admins, "id", "email", items.id_admin);
            ViewData["id_category"] = new SelectList(_context.Categories, "id", "name", items.id_category);
            return View(items);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,vi_info,en_info,img,volatility,trade_min,trade_max,active,lowest_ask,highest_bid,sold,id_admin,id_category")] Items items)
        {
            if (id != items.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(items);
                    await _context.SaveChangesAsync();
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
            ViewData["id_admin"] = new SelectList(_context.Admins, "id", "email", items.id_admin);
            ViewData["id_category"] = new SelectList(_context.Categories, "id", "name", items.id_category);
            return View(items);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .Include(i => i.admin)
                .Include(i => i.category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.Items.FindAsync(id);
            _context.Items.Remove(items);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.id == id);
        }
    }
}
