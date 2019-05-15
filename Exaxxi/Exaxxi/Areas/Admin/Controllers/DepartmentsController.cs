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
using Microsoft.AspNetCore.Authorization;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
       
        CallAPI _api = new CallAPI();
       
        // GET: Admin/Departments
        public IActionResult Index()
        {
            IEnumerable<Departments> result = JsonConvert.DeserializeObject<List<Departments>>(_api.getAPI("api/DepartmentsAPI").Result);
            return View(result);
        }

        // GET: Admin/Departments/Details/5
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

        // GET: Admin/Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,vi_name,en_name,active,order")] Departments departments)
        {
            if (_api.postAPI(departments, "api/DepartmentsAPI").Result)
            {
                
                return RedirectToAction(nameof(Index));
            }
            return View("Create");
        }

        // GET: Admin/Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departments = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/DepartmentsAPI/{id}").Result);
            if (departments == null)
            {
                return NotFound();
            }
            return View(departments);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,vi_name,en_name,active,order")] Departments departments)
        {
            if (id != departments.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(departments, $"api/DepartmentsApi/{id}");
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
        private bool DepartmentsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/DepartmentsApi/DepartmentsExists/{id}").Result);
        }
    }
}
