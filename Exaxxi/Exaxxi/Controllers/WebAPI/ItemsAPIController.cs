using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ItemsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public IEnumerable<Items> GetItems()
        {
            return _context.Items;
        }

        [Route("TakeAllItemByIdBrand/{Id_Brand}")]
        public IEnumerable<Items> GetAllItemByIdBrand(int Id_Brand)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(g => g.d.id ==  Id_Brand)
                .Select(p => p.c.a);
        }
        
        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _context.Items.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }
        [Route("Popular/{id_depart}")]
        public IEnumerable<ItemViewModel> GetItemsPopular(int id_depart)
        {
            return _context.Posts
                .Join(_context.Sizes, g => g.id_size, h => h.id, (g, h) => new { g, h })
                .Join(_context.Items, e => e.h.id_item, f => f.id, (e, f) => new { e, f })
                .Join(_context.Categories, a => a.f.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.f.active == true && k.d.id == id_depart)
                .OrderBy(h => h.c.a.f.sold)
                .Take(10)
                .Select(m => new ItemViewModel
                {
                    item = m.c.a.f,
                    id_post = m.c.a.e.g.id,
                    create_at = m.c.a.e.g.date_start
                });
        }
        [Route("ProductDetail")]
        public IActionResult ProductDetail()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = _context.Items;

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }
        [Route("Search")]
        public IEnumerable<ItemViewModel> search()
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Select(p => new ItemViewModel
                {
                    item = p.c.a,
                    brand_name = p.d.name,
                    cate_name = p.c.b.name
                }
                );
        }        

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems([FromRoute] int id, [FromBody] Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != items.id)
            {
                return BadRequest();
            }

            _context.Entry(items).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Items
        [HttpPost]
        public async Task<IActionResult> PostItems([FromBody] Items items)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.Add(items);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItems", new { id = items.id }, items);
        }

        [Route("TakeIdCategory_Checkbox")]
        public IEnumerable<Items> TakeIdCategory_Checkbox([FromBody] JArray json)
        {
            List<int> value = json.ToObject<List<int>>();

            return _context.Items.Where(x => value.Contains(x.id_category)).OrderBy(x => x.name).ToList();          
        }
        
        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItems([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            _context.Items.Remove(items);
            await _context.SaveChangesAsync();

            return Ok(items);
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.id == id);
        }
    }
}