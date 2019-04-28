using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.Controllers.WebAPI;
using System.Diagnostics;
using Exaxxi.Helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Exaxxi.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ExaxxiDbContext _context;
        private DepartmentsAPIController de;

        //helper
        DepartmentsAPI _api = new DepartmentsAPI();

        public Stopwatch watch = new Stopwatch();
        public DepartmentsController(ExaxxiDbContext context)
        {
            watch.Start();
            _context = context;
            de = new DepartmentsAPIController(_context);
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            IEnumerable<Departments> resuit = new List<Departments>();
            resuit = de.GetDepartments();
            watch.Stop();
            ViewBag.time = watch.ElapsedMilliseconds.ToString();
            return View(resuit);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .FirstOrDefaultAsync(m => m.id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,active,order")] Departments departments)
        {            
            //await de.PostDepartments(departments);
            HttpClient client = _api.Initial();
            var stringContent = new StringContent(JsonConvert.SerializeObject(departments), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("api/DepartmentsAPI", stringContent);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                Departments dep = JsonConvert.DeserializeObject<Departments>(result);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }
            return View(departments);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,active,order")] Departments departments)
        {
            if (id != departments.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentsExists(departments.id))
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
            return View(departments);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = await _context.Departments
                .FirstOrDefaultAsync(m => m.id == id);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departments = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(departments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.id == id);
        }
    }
}
