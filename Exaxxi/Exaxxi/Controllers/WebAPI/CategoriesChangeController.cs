using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exaxxi.Controllers.WebAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public CategoriesChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories([FromRoute] int id, [FromBody] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categories.id)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategories([FromBody] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.id }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return Ok(categories);
        }
        [HttpGet("CategoriesExists/{id}")]
        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.id == id);
        }
    }
}