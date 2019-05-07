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
        CallAPI _api = new CallAPI();

        public DepartmentsController(ExaxxiDbContext context)
        {
            _context = context;
            de = new DepartmentsAPIController(_context);
        }

        // GET: Departments
        public IActionResult Index()
        {
            IEnumerable<Departments> resuit = JsonConvert.DeserializeObject<List<Departments>>(_api.getAPI("api/DepartmentsAPI").Result);
            ViewBag.test = DepartmentsExists(6);
            return View(resuit);
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = JsonConvert.DeserializeObject<Departments>(_api.getAPI($"api/DepartmentsAPI/{id}").Result);
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
        public IActionResult Create([Bind("id,name,active,order")] Departments departments)
        {            
            //await de.PostDepartments(departments);
            
            if (_api.postAPI(departments, "api/DepartmentsAPI").Result)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = JsonConvert.DeserializeObject<Departments>(_api.getAPI($"api/DepartmentsAPI/{id}").Result);
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
                    var result = await _api.putAPI(departments, $"api/DepartmentsAPI/{id}");
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = JsonConvert.DeserializeObject<Departments>(_api.getAPI($"api/DepartmentsAPI/{id}").Result);
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
            var result = await _api.deleteAPI($"api/DepartmentsAPI/{id}");
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentsExists(int id)
        {

            return _api.existAPI($"api/DepartmentsAPI/{id}/Exist").Result;
        }
    }
}
