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
            return _context.Sizes;
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

        private bool SizesExists(int id)
        {
            return _context.Sizes.Any(e => e.id == id);
        }
    }
}