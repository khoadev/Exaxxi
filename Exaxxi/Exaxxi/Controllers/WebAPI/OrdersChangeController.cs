using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public OrdersChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdersChange
        [HttpGet]
        public IEnumerable<Orders> GetOrders()
        {
            return _context.Orders;
        }

        // GET: api/OrdersChange/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = await _context.Orders.FindAsync(id);

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [Route("UpdateStatusOrder/{status}/{id}")]
        public async Task<IActionResult> UpdateStatusOrder(int status, int id)
        {            
            var data = _context.Orders.Where(p => p.id == id).First().status = status;            

            if(status == 2)
            {
                var sold = _context.Orders
                    .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                    .Join(_context.Sizes, c => c.b.id_size, d => d.id, (c, d) => new { c, d })
                    .Join(_context.Items, e => e.d.id_item, f => f.id, (e, f) => new { e, f })
                    .Where(p => p.e.c.a.id == id)
                    .FirstOrDefault().f.id;

                var upd_sold = _context.Items.Where(p => p.id == sold).First().sold += sold;
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/OrdersChange/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders([FromRoute] int id, [FromBody] Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orders.id)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
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

        // POST: api/OrdersChange
        [HttpPost]
        public async Task<IActionResult> PostOrders([FromBody] Orders orders)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(orders);

            //Update Post
            _context.Posts.First(p => p.id == orders.id_post).status=1;      

            await _context.SaveChangesAsync();

            return Ok();
        }        

        // DELETE: api/OrdersChange/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return Ok(orders);
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.id == id);
        }
    }
}