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
        public IEnumerable<Orders> GetOrders()
        {
            return _context.Orders.ToList();
        }

        [HttpGet("Detail/{status}/{id}")]
        public IEnumerable<OrderViewModel> GetDetail(int status, int id)
        {
            return _context.Orders
                .Join(_context.Users, a => a.id_user, b => b.id, (a, b) => new { a, b })
                .Join(_context.Posts, c => c.a.id_post, d => d.id, (c, d) => new { c, d })
                .Join(_context.Sizes, e => e.d.id_size, f => f.id, (e, f) => new { e, f })
                .Join(_context.Items, g => g.f.id_item, h => h.id, (g, h) => new { g, h })                
                .Where(z => z.g.e.c.a.id == id && z.g.e.c.a.status == status)
                .Select(p => new OrderViewModel
                {
                    orders = p.g.e.c.a,
                    items = p.h,
                    user = p.g.e.c.b,
                }).ToList();
        }

        [HttpGet("TakeVoucher/{id}")]
        public IActionResult TakeVoucher(int id)
        {
            var data = "Không";

            if (_context.Orders.Where(p => p.id == id).FirstOrDefault().id_voucher != 0)
            {                
                data = _context.Orders.Join(_context.Vouchers, a => a.id_voucher, b => b.id, (a, b) => new { a, b })
                .Where(z => z.a.id == id)
                .FirstOrDefault().b.code;
            }

            return Ok(data);
        }

        [HttpGet("TakeCity/{id}")]
        public IActionResult TakeCity(int id)
        {
            var data = _context.Orders
                .Join(_context.Citys, a => a.id_city, b => b.id, (a, b) => new { a, b })
                .Where(z => z.a.id == id)
                .FirstOrDefault().b.name;
            return Ok(data);
        }

        [Route("SelectKind/{kind}/{status}")]
        public IEnumerable<OrderViewModel> SelectKind(int kind, int status)
        {
            return _context.Orders
                .Join(_context.Users, a => a.id_user, b => b.id, (a, b) => new { a, b })
                .Join(_context.Posts, c => c.a.id_post, d => d.id, (c, d) => new { c, d })
                .Join(_context.Sizes, e => e.d.id_size, f => f.id, (e, f) => new { e, f })
                .Join(_context.Items, g => g.f.id_item, h => h.id, (g, h) => new { g, h })
                .Where(z => z.g.e.d.kind == kind && z.g.e.c.a.status == status)
                .Select(p => new OrderViewModel
                {
                    orders = p.g.e.c.a,
                    items = p.h,
                    user = p.g.e.c.b
                });
        }

        [Route("CountOrderBuying_Account/{idUser}")]
        public IActionResult CountOrderBuying_Account(int idUser)
        {
            var count = _context.Orders
                .Join(_context.Posts, a => a.id_post, b => b.id, (a, b) => new { a, b })
                .Where(p => p.a.id_user == idUser && p.b.kind == 1).Count();

            return Ok(count);
        }

        [Route("CountOrders")]
        public IActionResult CountOrders()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orders = _context.Orders.Where(p => p.status == 0).Count();

            return Ok(orders);
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