using System;
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

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        CallAPI _api = new CallAPI();
       

        // GET: Admin/News

        public IActionResult Index()
        {
            IEnumerable<News> result = JsonConvert.DeserializeObject<List<News>>(_api.getAPI("api/NewsAPI").Result);
            return View(result);
        }

        // GET: Admin/News/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = JsonConvert.DeserializeObject<News>(_api.getAPI($"api/NewsAPI/GetNewsDetail/{id}").Result);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
          
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,vi_title,en_title,vi_content,en_content,date_create,active,id_admin,id_department")] News news)
        {
            if (_api.postAPI(news, "api/NewsAPI").Result)
            {
                IEnumerable<News> result = JsonConvert.DeserializeObject<List<News>>(_api.getAPI("api/NewsAPI").Result);
                return RedirectToAction(nameof(Index));
            }
           
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = JsonConvert.DeserializeObject<News>(_api.getAPI($"api/NewsAPI/{id}").Result);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,vi_title,en_title,vi_content,en_content,date_create,active,id_admin,id_department")] News news)
        {
            if (id != news.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(news, $"api/NewsAPI/{id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.id))
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
           
            return View(news);
        }

        private bool NewsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/NewsAPI/NewsExists/{id}").Result);
        }
    }
}
