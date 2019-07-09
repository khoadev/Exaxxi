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
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {

        CallAPI _api = new CallAPI();

        // GET: Admin/Posts
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            IEnumerable<PostViewAdmin> result = JsonConvert.DeserializeObject<List<PostViewAdmin>>(_api.getAPI("api/PostsAPI", HttpContext.Session.GetString("token")).Result);
            return View(result);
        }

        // GET: Admin/Posts/Details/5
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

            var posts = JsonConvert.DeserializeObject<Posts>(_api.getAPI($"api/PostsAPI/GetPostsDetail/{id}", HttpContext.Session.GetString("token")).Result);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,price,date_start,date_end,kind,id_size,id_user")] Posts posts)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (_api.postAPI(posts, "api/PostsChange", HttpContext.Session.GetString("token")).Result)
            {
                IEnumerable<Posts> result = JsonConvert.DeserializeObject<List<Posts>>(_api.getAPI("api/PostsAPI", HttpContext.Session.GetString("token")).Result);
                return RedirectToAction(nameof(Index));
            }

            return View(posts);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return NotFound();
            }

            var posts = JsonConvert.DeserializeObject<Posts>(_api.getAPI($"api/PostsAPI/{id}", HttpContext.Session.GetString("token")).Result);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,price,date_start,date_end,kind,id_size,id_user")] Posts posts)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id != posts.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(posts, $"api/PostsChange/{id}", HttpContext.Session.GetString("token"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.id))
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

            return View(posts);
        }
        private bool PostsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/PostsAPI/PostsExists/{id}", HttpContext.Session.GetString("token")).Result);
        }
    }
}
