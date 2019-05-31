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
    public class ds_SizeAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ds_SizeAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/ds_Size
        [HttpGet]
        public IEnumerable<ds_Size> Getds_Size()
        {
            return _context.ds_Size;
        }

        // GET: api/ds_Size/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getds_Size([FromRoute] int id)
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

            return Ok(ds_Size);
        }

       
    }
}