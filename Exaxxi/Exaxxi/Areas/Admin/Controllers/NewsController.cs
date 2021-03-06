﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Exaxxi.Helper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class NewsController : Controller
    {
        CallAPI _api = new CallAPI();
        // GET: Admin/News

        public IActionResult Index()
        {

            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<News> result = JsonConvert.DeserializeObject<List<News>>(_api.getAPI("api/NewsAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }


        // GET: Admin/News/Details/5
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

            var news = JsonConvert.DeserializeObject<News>(_api.getAPI($"api/NewsAPI/GetNewsDetail/{id}", HttpContext.Session.GetString("token")).Result);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile img, [Bind("vi_title,en_title,img,vi_content,en_content,date_create,active,id_admin,id_department")] News news)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            //Nhận file POST qua
            if (img == null || img.Length == 0)
                return Content("Không File nào được chọn!");

            //Save File da upload vao thu muc MyFiles
            string fullname = Path.Combine
                (Directory.GetCurrentDirectory(), "wwwroot", "images", "news", img.FileName);

            using (var myfile = new FileStream(fullname, FileMode.Create))
            {
                img.CopyTo(myfile);
            }

            //Gán tên file vào img để lưu vào DB
            news.img = img.FileName;
            DateTime aDateTime = DateTime.Now;
            news.date_create = aDateTime;
            //Post sang API xử lý
            if (_api.postAPI(news, "api/NewsChange", HttpContext.Session.GetString("token")).Result)
            {

                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Admin/News/Edit/5
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

            var news = JsonConvert.DeserializeObject<News>(_api.getAPI($"api/NewsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, News news, IFormFile img)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != news.id)
            {
                return NotFound();
            }


            try
            {
                //Nhận file POST qua
                if (img == null || img.Length == 0)
                {
                    var nameImg = _api.getAPI($"api/NewsAPI/img/{id}", HttpContext.Session.GetString("token")).Result;
                    news.img = nameImg.ToString();

                }
                else
                {
                    //Save File da upload vao thu muc MyFiles
                    string fullname = Path.Combine
                        (Directory.GetCurrentDirectory(), "wwwroot", "images", "news", img.FileName);

                    using (var myfile = new FileStream(fullname, FileMode.Create))
                    {
                        img.CopyTo(myfile);
                    }
                    news.img = img.FileName;
                }

                
                DateTime aDateTime = DateTime.Now;
                news.date_create = aDateTime;
                var result = _api.putAPI(news, $"api/NewsChange/{id}", HttpContext.Session.GetString("token"));
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateConcurrencyException)
            {
                    return View(news);                             
            }

           
        }
        private bool NewsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/NewsAPI/NewsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }


    }
}
