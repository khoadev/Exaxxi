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
using Microsoft.AspNetCore.Http;

namespace Exaxxi.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class DepartmentsController : Controller
    {

        CallAPI _api = new CallAPI();

        // GET: Admin/Departments
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Departments> result = JsonConvert.DeserializeObject<List<Departments>>(_api.getAPI("api/DepartmentsAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Departments/Details/5
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

            var departments = JsonConvert.DeserializeObject<Departments>(_api.getAPI($"api/DepartmentsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (departments == null)
            {
                return NotFound();
            }

            return View(departments);
        }

        // GET: Admin/Departments/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,vi_name,en_name,active,order")] Departments departments)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (_api.postAPI(departments, "api/DepartmentsChange", HttpContext.Session.GetString("token")).Result)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(departments);
        }

        // GET: Admin/Departments/Edit/5
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

            var departments = JsonConvert.DeserializeObject<Departments>(_api.getAPI($"api/DepartmentsAPI/{id}", HttpContext.Session.GetString("token")).Result);
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
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != departments.id)
            {
                return NotFound();
            }


            try
            {
                var result = await _api.putAPI(departments, $"api/DepartmentsChange/{id}", HttpContext.Session.GetString("token"));
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(departments.id))
                {
                    return View(departments);
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }


            
        }
        private bool DepartmentsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/DepartmentsApi/DepartmentsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
