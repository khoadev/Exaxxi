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
    public class FollowingsChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public FollowingsChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Followings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowings([FromRoute] int id, [FromBody] Followings followings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followings.id)
            {
                return BadRequest();
            }

            _context.Entry(followings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingsExists(id))
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

        // POST: api/Followings
        [HttpPost]
        public async Task<IActionResult> PostFollowings([FromBody] Followings followings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Followings.Add(followings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowings", new { id = followings.id }, followings);
        }

        // DELETE: api/Followings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followings = await _context.Followings.FindAsync(id);
            if (followings == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(followings);
            await _context.SaveChangesAsync();

            return Ok(followings);
        }

        private bool FollowingsExists(int id)
        {
            return _context.Followings.Any(e => e.id == id);
        }
    }
}