using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.Helper;
using Exaxxi.Controllers.WebAPI;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using Exaxxi.ViewModels;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AdminsController : Controller
    {
        private readonly ExaxxiDbContext _context;
        private AdminsAPIController ad;

        CallAPI _api = new CallAPI();

        public AdminsController(ExaxxiDbContext context)
        {
            _context = context;
            ad = new AdminsAPIController(_context);
        }

        // GET: Admin/Admins
        public IActionResult Index()
        {
            IEnumerable<Admins> result = JsonConvert.DeserializeObject<List<Admins>>(_api.getAPI("api/AdminsAPI").Result);
            return View(result);
        }

        // GET: Admin/Admins/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/{id}").Result);

            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // GET: Admin/Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,username,password,email,level,date_create,active")] Admins admins, LoginViewModel model)
        {
            if (_api.postAPI(admins, "api/AdminsAPI").Result)
            {

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Message = "mnsdjfhjdshf";
                return View("Create");
            }

        }

        // GET: Admin/Admins/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/{id}").Result);
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }

        // POST: Admin/Admins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,email,level,date_create,active")] Admins admins)
        {
            if (id != admins.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(admins, $"api/AdminsAPI/{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminsExists(admins.id))
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
            return View(admins);
        }

        // GET: Admin/Admins/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/{id}").Result);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // POST: Admin/Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admins = await _context.Admins.FindAsync(id);
            _context.Admins.Remove(admins);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.id == id);
        }
    }
}
