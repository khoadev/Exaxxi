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
    public class ServiceFeeDetailsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ServiceFeeDetailsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceFeeDetailsAPI
        [HttpGet]
        public IEnumerable<ServiceFeeDetails> GetServiceFeeDetails()
        {
            return _context.ServiceFeeDetails;
        }

        // GET: api/ServiceFeeDetailsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceFeeDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceFeeDetails = await _context.ServiceFeeDetails.FindAsync(id);

            if (serviceFeeDetails == null)
            {
                return NotFound();
            }

            return Ok(serviceFeeDetails);
        }

        // PUT: api/ServiceFeeDetailsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceFeeDetails([FromRoute] int id, [FromBody] ServiceFeeDetails serviceFeeDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serviceFeeDetails.id)
            {
                return BadRequest();
            }

            _context.Entry(serviceFeeDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceFeeDetailsExists(id))
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

        // POST: api/ServiceFeeDetailsAPI
        [HttpPost]
        public async Task<IActionResult> PostServiceFeeDetails([FromBody] ServiceFeeDetails serviceFeeDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ServiceFeeDetails.Add(serviceFeeDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServiceFeeDetails", new { id = serviceFeeDetails.id }, serviceFeeDetails);
        }

        // DELETE: api/ServiceFeeDetailsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceFeeDetails([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceFeeDetails = await _context.ServiceFeeDetails.FindAsync(id);
            if (serviceFeeDetails == null)
            {
                return NotFound();
            }

            _context.ServiceFeeDetails.Remove(serviceFeeDetails);
            await _context.SaveChangesAsync();

            return Ok(serviceFeeDetails);
        }

        private bool ServiceFeeDetailsExists(int id)
        {
            return _context.ServiceFeeDetails.Any(e => e.id == id);
        }
    }
}