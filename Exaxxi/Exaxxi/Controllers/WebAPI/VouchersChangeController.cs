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

    public class VouchersChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public VouchersChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // PUT: api/VouchersChange/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucher([FromRoute] int id, [FromBody] Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voucher.id)
            {
                return BadRequest();
            }

            _context.Entry(voucher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(id))
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

        // POST: api/VouchersChange
        [HttpPost]
        public async Task<IActionResult> PostVoucher([FromBody] Voucher voucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVoucher", new { id = voucher.id }, voucher);
        }

        // DELETE: api/VouchersChange/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }

            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();

            return Ok(voucher);
        }

        [HttpGet("VouchersExists/{id}")]
        private bool VoucherExists(int id)
        {
            return _context.Vouchers.Any(e => e.id == id);
        }
    }
}