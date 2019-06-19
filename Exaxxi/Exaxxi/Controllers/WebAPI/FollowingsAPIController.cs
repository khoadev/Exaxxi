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

        
    }
}