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
using Newtonsoft.Json;
using Exaxxi.Helper;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        CallAPI _api = new CallAPI();

        public PostsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<PostViewAdmin> GetPosts()
        {
            return _context.Posts
                .Join(_context.Sizes, a => a.id_size, b => b.id, (a, b) => new { a, b })
                .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                .Join(_context.Items, e => e.c.b.id_item, f => f.id, (e, f) => new { e, f })
                .Join(_context.Users, g => g.e.c.a.id_user, h => h.id, (g, h) => new { g, h })
                .Select(p => new PostViewAdmin
                {
                    post = p.g.e.c.a,
                    size = p.g.e.d.VN,
                    nameItem = p.g.f.name,
                    usernameSell = p.h.name,
                    
                }).Where(i => i.post.status == 0);
        }

        [HttpGet("GetPostsSell")]
        public IEnumerable<PostViewAdmin> GetPostsSell()
        {
            return _context.Posts
                .Join(_context.Sizes, a => a.id_size, b => b.id, (a, b) => new { a, b })
                .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                .Join(_context.Items, e => e.c.b.id_item, f => f.id, (e, f) => new { e, f })
                .Join(_context.Users, g => g.e.c.a.id_user, h => h.id, (g, h) => new { g, h })
                .Join(_context.Orders, i => i.h.id, k => k.id_post, (i, k) => new { i, k })
                .Select(p => new PostViewAdmin
                {
                    post = p.i.g.e.c.a,
                    size = p.i.g.e.d.VN,
                    nameItem = p.i.g.f.name,
                    usernameSell = p.i.h.name,
                    order = p.k
                }).Where(i => i.post.status == 1);
        }

        [HttpGet("GetNameBuy")]
        public IEnumerable<Users> GetNameBuy()
        {
            return _context.Users;
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
        [Route("TakeAsk/{id}")]
        public IEnumerable<PostViewModel> takeask(int id)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f }).Distinct()
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.c.a.e.g.id_user == id && k.c.a.e.g.kind==1)
                .Select(m => new PostViewModel
                {
                    post =m.c.a.e.g,
                    item = m.c.a.f,
                    
                });

        }
        [Route("TakeBid/{id}")]
        public IEnumerable<PostViewModel> takebid(int id)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f }).Distinct()
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.c.a.e.g.id_user == id && k.c.a.e.g.kind == 2)
                .Select(m => new PostViewModel
                {
                    post = m.c.a.e.g,
                    item = m.c.a.f,

                });

        }
        [Route("CountPosts")]
        public IActionResult CountPosts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posts = _context.Posts.Count();

            return Ok(posts);
        }

        //[Route("Popular/{id_depart}")]
        //public IEnumerable<PostViewModel> GetItemsPopular(int id_depart)
        //{
        //    return _context.Posts
        //        .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
        //        .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f }).Distinct()
        //        .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
        //        .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
        //        .Where(k => k.c.a.f.active == true && k.d.id_department == id_depart)
        //        .OrderBy(h => h.c.a.f.sold)
        //        .Take(10)
        //        .Select(m => new PostViewModel
        //        {
        //            item = m.c.a.f,
        //            id_post = m.c.a.e.g.id,
        //            create_at = m.c.a.e.g.date_start
        //        });
        //}
        [Route("LowestAskNew/{id_depart}")]
        public IEnumerable<PostViewModel> GetItemsLowestAsk(int id_depart)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f }).Distinct()
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.d.id_department == id_depart && k.c.a.e.g.kind == 1 && k.c.a.e.g.price == k.c.a.e.h.lowest_ask && k.c.a.e.g.status == 0)
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
        [Route("HighestBidNew/{id_depart}")]
        public IEnumerable<PostViewModel> GetItemsHighestBid(int id_depart)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f }).Distinct()
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.d.id_department == id_depart && k.c.a.e.g.kind == 2 && k.c.a.e.g.price == k.c.a.e.h.highest_bid && k.c.a.e.g.status == 0)
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

        [Route("SelectEmailUser/{idPost}")]
        public IActionResult SelectEmailUser(int idPost)
        {
            var data = _context.Posts
                .Join(_context.Users, a => a.id_user, b => b.id, (a, b) => new { a, b })
                .Where(p => p.a.id == idPost).SingleOrDefault().b.email;     
            return Ok(data);
        }
        [Route("TakeLowestAsk/{id_item}/{id_size}")]
        public IActionResult TakeLowestAsk(int id_item, int id_size)
        {
            double lowestAsk = 0;
            if(!ModelState.IsValid || id_item == 0)
            {
                return BadRequest(ModelState);
            }
            if(id_item > 0)
            {
                var list = _context.Posts.Join(_context.Sizes, p => p.id_size, s => s.id, (p, s) => new { p, s })
                                                  .Where(r => r.s.id_item == id_item && r.p.kind == 1 && r.p.status == 0);
                if(id_size > 0)
                {
                    list = list.Where(h => h.s.id == id_size);
                }
                if (list.Count() > 0) lowestAsk = list.OrderBy(o => o.p.price).FirstOrDefault().p.price;
                else return Ok(0);
            }
            return Ok(lowestAsk);
        }
        [Route("TakeHighestBid/{id_item}/{id_size}")]
        public IActionResult TakeHighestBid(int id_item, int id_size)
        {
            double highestBid = 0;
            if (!ModelState.IsValid || id_item == 0)
            {
                return BadRequest(ModelState);
            }
            if (id_item > 0)
            {
                var list = _context.Posts.Join(_context.Sizes, p => p.id_size, s => s.id, (p, s) => new { p, s })
                                                  .Where(r => r.s.id_item == id_item && r.p.kind == 2 && r.p.status == 0);
                if (id_size > 0)
                {
                    list = list.Where(h => h.s.id == id_size);
                }
                if (list.Count() > 0) highestBid = list.OrderByDescending(o => o.p.price).FirstOrDefault().p.price;
                else return Ok(0);
            }
            return Ok(highestBid);
        }
        [Route("FindPostMatchForAsk/{id_size}/{price}")]
        public IActionResult FindPostMatchForAsk([FromRoute] int id_size,[FromRoute] double price)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var post = _context.Posts.Include(p => p.user).Where(r => r.id_size == id_size && r.price >= price && r.kind == 2 && r.status == 0).OrderBy(o => o.price).FirstOrDefault();

            if (post == null) return NotFound();

            return Ok(post);
        }
    }
}