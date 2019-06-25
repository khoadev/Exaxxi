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

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ExaxxiDbContext _context;
        CallAPI _api = new CallAPI();
        public PostsController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Posts
        public IActionResult Index()
        {
            IEnumerable<PostViewAdmin> result = JsonConvert.DeserializeObject<List<PostViewAdmin>>(_api.getAPI("api/PostsAPI").Result);
            return View(result);
        }

        // GET: Admin/Posts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = JsonConvert.DeserializeObject<Posts>(_api.getAPI($"api/PostsAPI/GetPostsDetail/{id}").Result);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Admin/Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("id,price,date_start,date_end,kind,id_size,id_user")] Posts posts)
        {
            if (_api.postAPI(posts, "api/PostsAPI").Result)
            {
                IEnumerable<Posts> result = JsonConvert.DeserializeObject<List<Posts>>(_api.getAPI("api/PostsAPI").Result);
                return RedirectToAction(nameof(Index));
            }
           
            return View(posts);
        }

        // GET: Admin/Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts.FindAsync(id);
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
            if (id != posts.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _api.putAPI(posts, $"api/PostsAPI/{id}");
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
            ViewData["id_size"] = new SelectList(_context.Sizes, "id", "id", posts.id_size);
            ViewData["id_user"] = new SelectList(_context.Users, "id", "email", posts.id_user);
            return View(posts);
        }
        private bool PostsExists(int id)
        {
            return JsonConvert.DeserializeObject<bool>(_api.getAPI($"api/PostsAPI/PostsExists/{id}").Result);
        }
    }
}
