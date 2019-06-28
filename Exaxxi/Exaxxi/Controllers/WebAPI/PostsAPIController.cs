using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public PostsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<PostViewAdmin> GetPosts()
        {
            return _context.Posts.Include("user").Join(_context.Sizes, a => a.id_size, b => b.id, (a, b) => new { a, b }).Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d }).Select(
                p => new PostViewAdmin { post = p.c.a, size = p.d.VN });
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posts = await _context.Posts.FindAsync(id);

            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }
        [Route("Search")]
        public IEnumerable<PostViewModel> search()
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Select(p => new PostViewModel
                {
                    item = p.c.a,
                    brand_name = p.d.name,
                    cate_name = p.c.b.name
                }
                );
        }
        [Route("Popular/{id_depart}")]
        public IEnumerable<PostViewModel> GetItemsPopular(int id_depart)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f })
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.d.id_department == id_depart)
                .OrderBy(h => h.c.a.f.sold)
                .Take(10)
                .Select(m => new PostViewModel
                {
                    item = m.c.a.f,
                    id_post = m.c.a.e.g.id,
                    create_at = m.c.a.e.g.date_start
                });
        }
        [Route("LowestAskNew/{id_depart}")]
        public IEnumerable<PostViewModel> GetItemsLowestAsk(int id_depart)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f })
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.d.id_department == id_depart && k.c.a.e.g.kind == 1 && k.c.a.e.g.price == k.c.a.e.h.lowest_ask)
                .OrderByDescending(h => h.c.a.e.g.date_start)
                .Take(10)
                .Select(m => new PostViewModel
                {
                    item = m.c.a.f,
                    id_post = m.c.a.e.g.id,
                    create_at = m.c.a.e.g.date_start,
                    price = m.c.a.e.g.price,
                    kind = m.c.a.e.g.kind
                });
        }

    }
}