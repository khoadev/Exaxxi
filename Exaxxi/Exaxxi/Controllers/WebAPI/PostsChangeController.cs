using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Common;
using Exaxxi.Helper;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        CallAPI _api = new CallAPI();

        public PostsChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosts([FromRoute] int id, [FromBody] Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posts.id)
            {
                return BadRequest();
            }

            _context.Entry(posts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
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

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPosts([FromBody] Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Posts.Add(posts);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posts = await _context.Posts.FindAsync(id);

            if (posts == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(posts);
            await _context.SaveChangesAsync();

            return Ok(posts);
        }
        [HttpGet("PostsExists/{id}")]
        private bool PostsExists(int id)
        {
            return _context.Posts.Any(e => e.id == id);
        }
        
    }
}