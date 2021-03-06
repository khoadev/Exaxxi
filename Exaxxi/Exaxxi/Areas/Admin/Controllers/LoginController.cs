﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Helper;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Exaxxi.Common;
using Microsoft.AspNetCore.Http;

namespace Exaxxi.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class LoginController : Controller
    {
        //helper
        CallAPI _api = new CallAPI();
        BlowFish bf = new BlowFish(info.keyBF);
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]
        [Route("Admin")]
        public IActionResult Login(string returnUrl = "")
        {

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/GetDetailAdmins/{id}", HttpContext.Session.GetString("token")).Result);
            ViewBag.Password = bf.Decrypt_CBC(admins.password);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("idAdmin");
            HttpContext.Session.Remove("nameAdmin");
            HttpContext.Session.Remove("levelAdmin");
            HttpContext.Session.Remove("token");
            return RedirectToAction("Login");
        }
        [HttpPost]
        public string setTokenSession([FromBody] string token)
        {
            HttpContext.Session.SetString("token", token);
            return HttpContext.Session.GetString("token");
        }
    }
}