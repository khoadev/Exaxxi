using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public OrdersAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok();
        }

        [Route("CountOrderBuying_Account/{idUser}")]
        public IActionResult CountOrderBuying_Account(int idUser)
        {
            var count = _context.Orders
                .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                .Where(p => p.a.id_user == idUser && p.b.kind == 1).Count();

            return Ok(count);
        }

        [Route("TakeDetailBuying_Account/{kind}/{idUser}")]
        public IActionResult TakeDetailBuying_Account(int kind, int idUser)
        {
            var data = _context.Orders
                .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                .Join(_context.Sizes, c => c.b.id_size, d => d.id, (c, d) => new { c, d })
                .Join(_context.Items, e => e.d.id_item, f => f.id, (e, f) => new { e, f })
                .Where(p => p.e.c.a.id_user == idUser && p.e.c.a.status == kind && p.e.c.b.kind == 1)
                .Select(g => new OrderViewModel {
                    items = g.f,
                    orders = g.e.c.a,
                    size = g.e.d
                });

            return Ok(data);
        }

        [Route("CountOrderSelling_Account/{idUser}")]
        public IActionResult CountOrderSelling_Account(int idUser)
        {
            var count = _context.Orders
                .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                .Where(p => p.a.id_user == idUser && p.b.kind == 2).Count();

            return Ok(count);
        }

        [Route("TakeDetailSelling_Account/{kind}/{idUser}")]
        public IActionResult TakeDetailSelling_Account(int kind, int idUser)
        {
            var data = _context.Orders
                .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                .Join(_context.Sizes, c => c.b.id_size, d => d.id, (c, d) => new { c, d })
                .Join(_context.Items, e => e.d.id_item, f => f.id, (e, f) => new { e, f })
                .Where(p => p.e.c.a.id_user == idUser && p.e.c.a.status == kind && p.e.c.b.kind == 2)
                .Select(g => new OrderViewModel {
                    items = g.f,
                    orders = g.e.c.a,
                    size = g.e.d
                });

            return Ok(data);
        }
    }
}