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

        [HttpGet("TakeFirst/{id}")]
        public IActionResult TakeFirst(int id)
        {            
            var data = _context.ds_Size
                .Join(_context.Sizes, a => a.id, b => b.id_ds_size, (a, b) => new { a, b })
                .Join(_context.Items, c => c.b.id_item, d => d.id, (c, d) => new { c, d })
                .Where(g => g.d.id == id)
                .Select(p => p.c.a).FirstOrDefault().VN;

            return Ok(data);
        }

        [HttpGet("Takeds_SizeDepart/{id_Depart}")]
        public IEnumerable<ds_Size> Getds_SizeOfDepart([FromRoute]int id_Depart)
        {
            return _context.ds_Size.Where(s => s.id_Depart == id_Depart).OrderBy(s => s.VN);
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
        [HttpGet("TakeSize/{id}/{Size}")]
        public IActionResult TakeSize(int id, int size)
        {
            var data = _context.ds_Size
                .Join(_context.Sizes, a => a.id, b => b.id_ds_size, (a, b) => new { a, b })
                .Join(_context.Items, c => c.b.id_item, d => d.id, (c, d) => new { c, d })
                .Where(g => g.d.id == id && g.c.a.VN == size)
                .Select(p => p.c.a).FirstOrDefault().VN;

            return Ok(data);
        }
        [HttpGet("Getds_SizeDetail/{id}")]
        public async Task<IActionResult> Getds_SizeDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ds_size = await _context.ds_Size.FirstOrDefaultAsync(m => m.id == id);


            if (ds_size == null)
            {
                return NotFound();
            }

            return Ok(ds_size);
        }
        
        [HttpGet("ds_SizeExists/{id}")]
        private bool ds_SizeExists(int id)
        {
            return _context.ds_Size.Any(e => e.id == id);
        }
    }
}