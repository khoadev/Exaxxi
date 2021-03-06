﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Exaxxi.Helper;
using Exaxxi.Common;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        CallAPI _api = new CallAPI();
        BlowFish bf = new BlowFish(info.keyBF);

        // GET: Admin/Users
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Users> result = JsonConvert.DeserializeObject<List<Users>>(_api.getAPI("api/UsersAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Users/Edit/5
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

            var users = JsonConvert.DeserializeObject<Users>(_api.getAPI($"api/UsersAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Users users)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != users.id)
            {
                return NotFound();
            }            

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(users, $"api/UsersChange/{id}", HttpContext.Session.GetString("token"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.id))
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
            return View(users);
        }

        private bool UsersExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/UsersAPI/UsersExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
