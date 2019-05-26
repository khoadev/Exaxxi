﻿using System;
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

        [Route("TakeAllItemByIdCate/{Id_Cate}")]
        public IEnumerable<Items> GetAllItem(int Id_Cate)
        {
            return _context.Items.Where(p => p.id_category == Id_Cate);
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
        public IEnumerable<Items> GetItemsPopular(int id_depart)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Join(_context.Departments, e => e.d.id_department, f => f.id, (e, f) => new { e, f })
                .Where(g => g.e.c.a.active == true && g.f.id == id_depart)
                .OrderBy(h => h.e.c.a.sold)
                .Take(10)
                .Select(m => new Items
                {
                    id = m.e.c.a.id,
                    name = m.e.c.a.name,
                    vi_info = m.e.c.a.vi_info,
                    en_info = m.e.c.a.en_info,
                    img = m.e.c.a.img,
                    sold = m.e.c.a.sold,
                    id_category = m.e.c.a.id_category,
                    lowest_ask = m.e.c.a.lowest_ask
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
        public IEnumerable<SearchIteamModel> search()
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a,b})
                .Join(_context.Brands,c=>c.b.id_brand,d=>d.id,(c,d)=>new { c,d})
                .Select(p => new SearchIteamModel
                {
                    id=p.c.a.id,
                    name= p.c.a.name,
                    name_brand=p.d.name,
                    name_cate=p.c.b.name,
                    img=p.c.a.img
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

            return _context.Items.Where(x => value.Contains(x.id_category))
                .OrderBy(x => x.name).ToList();           
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