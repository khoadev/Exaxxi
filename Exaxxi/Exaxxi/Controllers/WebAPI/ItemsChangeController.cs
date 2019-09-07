using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Controllers.WebAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ItemsChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        [Route("FolderItems/{id}")]
        public IActionResult FolderItems(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Items.First(p => p.id == id).folder = "0";
            _context.SaveChanges();
           
            return Ok();
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
        
        // update lowest ask and highest bid of item
        [AllowAnonymous]
        [Route("UpdatePrice")]
        public async Task<IActionResult> UpdatePrice([FromBody]UpdatePrice model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _context.Items.Where(r => r.id == model.id).FirstOrDefault();
            item.lowest_ask = model.lowest_ask;
            item.highest_bid = model.highest_bid;
            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(model.id))
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
            var id_brand = _context.Categories.Where(p => p.id == items.id_category).FirstOrDefault().id_brand;
            var id_dep = _context.Brands.Where(g => g.id == id_brand).FirstOrDefault().id_department;

            var id_ds_size = _context.ds_Size.Where(h => h.id_Depart == id_dep).FirstOrDefault().id;
            
            //Save Sizes
            Sizes sizes = new Sizes();
            sizes.lowest_ask = 0;
            sizes.highest_bid = 0;
            sizes.last_sale = 0;
            sizes.id_ds_size = id_ds_size;
            sizes.id_item = items.id;

            _context.Sizes.Add(sizes);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItems", new { id = items.id }, items);
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