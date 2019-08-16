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
using Exaxxi.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AdminsController : Controller
    {
        CallAPI _api = new CallAPI();
        BlowFish bf = new BlowFish(info.keyBF);
        // GET: Admin/Admins

        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<Admins> result = JsonConvert.DeserializeObject<List<Admins>>(_api.getAPI("api/AdminsAPI", HttpContext.Session.GetString("token")).Result);

            return View(result);
        }

        // GET: Admin/Admins/Details/5
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

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            ViewBag.Password = bf.Decrypt_CBC(admins.password);
            if (admins == null)
            {
                return NotFound();
            }

            return View(admins);
        }

        // GET: Admin/Admins/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Admins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,name,password,email,level,date_create,active")] Admins admins)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            //Select mail
            if(_api.getAPI($"api/AdminsAPI/GetEmailAdmin/{admins.email}", HttpContext.Session.GetString("token")).Result != null)
            {
                if (admins.email == _api.getAPI($"api/AdminsAPI/GetEmailAdmin/{admins.email}", HttpContext.Session.GetString("token")).Result)
                {
                    ViewBag.EmailExist = "Email này đã tồn tại!";
                    return View("Create");
                }
            }
            
            if (_api.postAPI(admins, "api/AdminsAPI", HttpContext.Session.GetString("token")).Result)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Create");
            }

        }

        // GET: Admin/Admins/Edit/5
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

            var admins = JsonConvert.DeserializeObject<Admins>(_api.getAPI($"api/AdminsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (admins == null)
            {
                return NotFound();
            }
            return View(admins);
        }        

        private bool AdminsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/AdminsAPI/AdminsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }

        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ChangePassword(string email, string hash)
        {
            ViewBag.Email = email;
            ViewBag.Hash = hash;
            return View();
        }
    }
}
