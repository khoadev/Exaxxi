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
            return _context.Sizes.Include("item").Include("size");
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

        [HttpGet("GetSizesDetail/{id}")]
        public async Task<IActionResult> GetSizesDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var size = await _context.Sizes.Include(c => c.item).Include(p => p.size)
                .FirstOrDefaultAsync(m => m.id == id);

            if (size == null)
            {
                return NotFound();
            }

            return Ok(size);
        }

        [HttpGet("TakeSizesItem/{idItem}")]
        public IEnumerable<Sizes> TakeSizesItem(int idItem)
        {
            return _context.Sizes.Include("size").Where(i => i.id_item == idItem).OrderBy(t => t.size.VN);
        }

    }
}