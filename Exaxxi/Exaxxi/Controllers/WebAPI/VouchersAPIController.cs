using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.ViewModels;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class VouchersAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public VouchersAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/VouchersAPI
        [HttpGet]
        public IEnumerable<Voucher> GetVouchers()
        {
            return _context.Vouchers;
        }

        // GET: api/VouchersAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucher([FromRoute] int id)
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

            return Ok(voucher);
        }
        [HttpGet("Check/{code}/{total}")]
        public IActionResult Check([FromRoute] string code,[FromRoute] string total)
        {
            Voucher voucher = _context.Vouchers.Where(p => p.code == code && p.count > 0 && p.date_end > DateTime.Now).FirstOrDefault();
            if (voucher == null)
            {
                return NotFound();
            }
            if (voucher.kind == 1)
            {
                VoucherProcess vc = new VoucherProcess
                {
                    id_voucher=voucher.id,
                    discount= Convert.ToDouble(total) * voucher.value,
                };
                return Ok(vc);
            }
            else
            {
                VoucherProcess vc = new VoucherProcess
                {
                    id_voucher = voucher.id,
                    discount = voucher.value,
                };
                return Ok(vc);
            }
           
           
        }
        //cap nhat voucher khi su dung thanh cong
        [HttpGet("UpdateCount/{id}")]
        public IActionResult UpdateCount([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var check = _context.Vouchers.Where(p => p.id == id).FirstOrDefault().count;
                var vc = _context.Vouchers.Where(p => p.id == id).First();
                vc.count = check - 1;
                _context.SaveChanges();

                return Ok();
            }
            
        }
        //// PUT: api/VouchersAPI/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVoucher([FromRoute] int id, [FromBody] Voucher voucher)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != voucher.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(voucher).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VoucherExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/VouchersAPI
        //[HttpPost]
        //public async Task<IActionResult> PostVoucher([FromBody] Voucher voucher)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Vouchers.Add(voucher);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetVoucher", new { id = voucher.id }, voucher);
        //}

        //// DELETE: api/VouchersAPI/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVoucher([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var voucher = await _context.Vouchers.FindAsync(id);
        //    if (voucher == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vouchers.Remove(voucher);
        //    await _context.SaveChangesAsync();

        //    return Ok(voucher);
        //}

        //private bool VoucherExists(int id)
        //{
        //    return _context.Vouchers.Any(e => e.id == id);
        //}
    }
}