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
    public class CitysAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public CitysAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/CitysAPI
        [HttpGet]
        public IEnumerable<Citys> GetCitys()
        {
            return _context.Citys.OrderBy(c => c.id);
        }

        // GET: api/CitysAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCitys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var citys = await _context.Citys.FindAsync(id);

            if (citys == null)
            {
                return NotFound();
            }

            return Ok(citys);
        }
        //Take shipping fee by id city
        [HttpGet("TakeShippingFee/{id_city}")]
        public IActionResult GetShippingFeeByIdCity([FromRoute] int id_city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var city = _context.Citys.Include("shipping").Where(c => c.id == id_city).FirstOrDefault();
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.shipping.shipping_fee);
        }
        // PUT: api/CitysAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitys([FromRoute] int id, [FromBody] Citys citys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != citys.id)
            {
                return BadRequest();
            }

            _context.Entry(citys).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitysExists(id))
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

        // POST: api/CitysAPI
        [HttpPost]
        public async Task<IActionResult> PostCitys([FromBody] Citys citys)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Citys.Add(citys);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCitys", new { id = citys.id }, citys);
        }

        // DELETE: api/CitysAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCitys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var citys = await _context.Citys.FindAsync(id);
            if (citys == null)
            {
                return NotFound();
            }

            _context.Citys.Remove(citys);
            await _context.SaveChangesAsync();

            return Ok(citys);
        }

        private bool CitysExists(int id)
        {
            return _context.Citys.Any(e => e.id == id);
        }
    }
}