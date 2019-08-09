using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.Helper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VouchersController : Controller
    {
        CallAPI _api = new CallAPI();
        private readonly ExaxxiDbContext _context;

        public VouchersController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Vouchers
        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View("index");
        }

        // GET: Admin/Vouchers/Details/5
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

            var voucher = JsonConvert.DeserializeObject<Voucher>(_api.getAPI($"api/VouchersAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Admin/Vouchers/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Vouchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,code,kind,value,date_start,date_end,count")] Voucher voucher)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (voucher.kind == 1 && voucher.value >= 1) voucher.value = voucher.value / 100;
            if (_api.postAPI(voucher, "api/VouchersChange", HttpContext.Session.GetString("token")).Result)
            {
               
                return RedirectToAction(nameof(Index));
            }
            return View(voucher);
        }

        // GET: Admin/Vouchers/Edit/5
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

            var voucher = JsonConvert.DeserializeObject<Voucher>(_api.getAPI($"api/VouchersAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (voucher == null)
            {
                return NotFound();
            }
            return View(voucher);
        }

        // POST: Admin/Vouchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,code,kind,value,date_start,date_end,count")] Voucher voucher)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != voucher.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var result = await _api.putAPI(voucher, $"api/VouchersChange/{id}", HttpContext.Session.GetString("token"));
               
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.id))
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
            return View(voucher);
        }
      
        private bool VoucherExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/VouchertsChange/VouchersExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
