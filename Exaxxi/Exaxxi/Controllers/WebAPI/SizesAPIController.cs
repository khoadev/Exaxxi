using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public SizesAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Sizes
        [HttpGet]
        public IEnumerable<Sizes> GetSizes()
        {
            return _context.Sizes.Include("item").Include("size");
        }
        
        // GET: api/Sizes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSizes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sizes = await _context.Sizes.FindAsync(id);

            if (sizes == null)
            {
                return NotFound();
            }

            return Ok(sizes);
        }

        [HttpGet("GetSizesDetail/{id}")]
        public async Task<IActionResult> GetSizesDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var size = await _context.Sizes.Include(c => c.item).Include(p => p.size)
                .FirstOrDefaultAsync(m => m.id == id);

            if (size == null)
            {
                return NotFound();
            }

            return Ok(size);
        }

        [HttpGet("TakeSizesItem/{idItem}")]
        public IEnumerable<Sizes> TakeSizesItem(int idItem)
        {
            return _context.Sizes.Include("size").Where(i => i.id_item == idItem).OrderBy(t => t.size.VN);
        }

        [HttpGet("TakeIdSize_ForBid/{idItem}/{size}")]
        public IActionResult TakeIdSize_ForBid(int idItem, int size)
        {
            var rel = _context.Sizes
                .Join(_context.Items, a => a.id_item, b => b.id, (a, b) => new { a, b })
                .Join(_context.ds_Size, c => c.a.id_ds_size, d => d.id, (c, d) => new { c, d })
                .Where(p => p.c.b.id == idItem && p.d.VN == size)
                .FirstOrDefault().c.a.id;

            return Ok(rel);
        }
        [Route("TakeIdPost_ForOrder/{id_item}/{size}/{kind}")]
        public IActionResult TakeIdPost_ForOrder(int id_item, int size, int kind)
        {
            var size_item = _context.Sizes.Include(d => d.size).Include(s => s.item).Where(p => p.id_item == id_item && p.size.VN == size).FirstOrDefault();
            double pricePost = 0;
            if (kind == 1) pricePost = size_item.lowest_ask; else if (kind == 2) pricePost = size_item.highest_bid;

            var idPost = _context.Posts
                .Join(_context.Sizes, a => a.id_size, b => b.id, (a, b) => new { a, b })
                .Join(_context.Items, c => c.b.id_item, d => d.id, (c, d) => new { c, d })
                .Where(p => p.d.id == id_item && p.c.a.price == pricePost && p.c.a.kind == kind && p.c.a.date_start <= DateTime.Now && p.c.a.date_end >= DateTime.Now);
            if (idPost.Count() == 0)
                return NotFound();
            else
            {
                
                var id_Post = idPost.Select(p => new Posts
                {
                    id = p.c.a.id
                }).FirstOrDefault().id;

                return Ok(id_Post);
            }
        }
        
    }
}