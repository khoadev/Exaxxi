using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ds_SizeChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ds_SizeChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/ds_Size/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putds_Size([FromRoute] int id, [FromBody] ds_Size ds_Size)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ds_Size.id)
            {
                return BadRequest();
            }

            _context.Entry(ds_Size).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ds_SizeExists(id))
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

        // POST: api/ds_Size
        [HttpPost]
        public async Task<IActionResult> Postds_Size([FromBody] ds_Size ds_Size)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ds_Size.Add(ds_Size);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getds_Size", new { id = ds_Size.id }, ds_Size);
        }

        // DELETE: api/ds_Size/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteds_Size([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ds_Size = await _context.ds_Size.FindAsync(id);
            if (ds_Size == null)
            {
                return NotFound();
            }

            _context.ds_Size.Remove(ds_Size);
            await _context.SaveChangesAsync();

            return Ok(ds_Size);
        }

        private bool ds_SizeExists(int id)
        {
            return _context.ds_Size.Any(e => e.id == id);
        }
    }
}