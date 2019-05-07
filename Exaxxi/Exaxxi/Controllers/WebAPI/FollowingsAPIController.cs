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
    public class FollowingsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public FollowingsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Followings
        [HttpGet]
        public IEnumerable<Followings> GetFollowings()
        {
            return _context.Followings;
        }

        // GET: api/Followings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowings([FromRoute] int id)
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

            return Ok(followings);
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