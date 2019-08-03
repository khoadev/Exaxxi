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

namespace Exaxxi.Controllers.WebAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SizesChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public SizesChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Sizes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSizes([FromRoute] int id, [FromBody] Sizes sizes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sizes.id)
            {
                return BadRequest();
            }

            _context.Entry(sizes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizesExists(id))
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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var size = _context.Sizes.Where(r => r.id == model.id).FirstOrDefault();
            size.lowest_ask = model.lowest_ask;
            size.highest_bid = model.highest_bid;
            _context.Entry(size).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SizesExists(model.id))
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
        // POST: api/Sizes
        [HttpPost]
        public async Task<IActionResult> PostSizes([FromBody] Sizes sizes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sizes.Add(sizes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSizes", new { id = sizes.id }, sizes);
        }

        // DELETE: api/Sizes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSizes([FromRoute] int id)
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

            _context.Sizes.Remove(sizes);
            await _context.SaveChangesAsync();

            return Ok(sizes);
        }
        [HttpGet("SizesExists/{id}")]
        private bool SizesExists(int id)
        {
            return _context.Sizes.Any(e => e.id == id);
        }
    }
}