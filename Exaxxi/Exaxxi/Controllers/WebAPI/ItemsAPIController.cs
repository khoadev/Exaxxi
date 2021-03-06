﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Newtonsoft.Json.Linq;
using Exaxxi.ViewModels;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public ItemsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet("GetItemsAd/{idcate}")]
        public IEnumerable<ItemViewAdmin> GetItemsAd(int idcate)
        {
            if (idcate == 0)
            {
                return _context.Items
                        .Join(_context.Admins, a => a.id_admin, b => b.id, (a, b) => new { a, b })
                        .Join(_context.Categories, c => c.a.id_category, d => d.id, (c, d) => new { c, d })
                        .Where(z => z.c.a.active == true)
                        .Select(g => new ItemViewAdmin
                        {
                            items = g.c.a,
                            nameAdmin = g.c.b.name,
                            nameCate = g.d.name

                        });
            }
            else
            {
                return _context.Items
                        .Join(_context.Admins, a => a.id_admin, b => b.id, (a, b) => new { a, b })
                        .Join(_context.Categories, c => c.a.id_category, d => d.id, (c, d) => new { c, d })
                        .Where(p => p.c.a.id_category == idcate && p.c.a.active == true)
                        .Select(g => new ItemViewAdmin
                        {
                            items = g.c.a,
                            nameAdmin = g.c.b.name,
                            nameCate = g.d.name

                        });
            }
        }
        // GET: api/Items
        [HttpGet("GetItemsIndex/{idcate}")]
        public IEnumerable<ItemViewAdmin> GetItemsIndex(int idcate)
        {
            if (idcate == 0)
            {
                return _context.Items
                        .Join(_context.Admins, a => a.id_admin, b => b.id, (a, b) => new { a, b })
                        .Join(_context.Categories, c => c.a.id_category, d => d.id, (c, d) => new { c, d })
                        .Select(g => new ItemViewAdmin
                        {
                            items = g.c.a,
                            nameAdmin = g.c.b.name,
                            nameCate = g.d.name

                        });
            }
            else
            {
                return _context.Items
                        .Join(_context.Admins, a => a.id_admin, b => b.id, (a, b) => new { a, b })
                        .Join(_context.Categories, c => c.a.id_category, d => d.id, (c, d) => new { c, d })
                        .Select(g => new ItemViewAdmin
                        {
                            items = g.c.a,
                            nameAdmin = g.c.b.name,
                            nameCate = g.d.name

                        });
            }
        }
        [HttpGet("img/{id}")]
        public IActionResult GetItemsImg(int id)
        {
            var data = _context.Items.Where(p => p.id == id).FirstOrDefault().img;
            return Ok(data);
        }
        // GET: api/Items
        [HttpGet("GetItemsEdit/{id}")]
        public async Task<IActionResult> GetItemsEdit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = await _context.Items.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }
        [HttpGet("GetNameItems/{id}")]
        public IActionResult GetNameItems(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = _context.Items.FirstOrDefault(p => p.id == id).name;

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [Route("CountItems")]
        public IActionResult CountItems()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = _context.Items.Count();

            return Ok(items);
        }

        [Route("TopItemSold")]
        public IActionResult TopItemSold()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = _context.Items.OrderByDescending(p => p.sold).Take(10);

            return Ok(items);
        }

        [Route("TakeItemByIdBrand/{Id_Brand}/{Qty}")]
        public IEnumerable<Items> GetAllItemByIdBrand(int Id_Brand, int Qty)
        {
            if (Qty == 0)
            {
                return _context.Items
                    .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                    .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                    .Where(g => g.d.id == Id_Brand)
                    .Select(p => p.c.a);
            }
            else
            {
                return _context.Items
                   .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                   .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                   .Where(g => g.d.id == Id_Brand)
                   .Select(p => p.c.a)
                   .Take(Qty);
            }
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public ItemViewAdmin GetItems(int id)
        {
            return _context.Items
                         .Join(_context.Admins, a => a.id_admin, b => b.id, (a, b) => new { a, b })
                         .Join(_context.Categories, c => c.a.id_category, d => d.id, (c, d) => new { c, d })
                         .Where(p => p.c.a.id == id && p.c.a.active == true)
                         .Select(g => new ItemViewAdmin
                         {
                             items = g.c.a,
                             nameAdmin = g.c.b.name,
                             nameCate = g.d.name

                         }).FirstOrDefault();
        }
        [HttpGet("TakeId_Depart/{id}")]
        public int TakeId_Depart(int id)
        {
            return _context.Items
                    .Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Join(_context.Categories, e => e.c.a.id_category, f => f.id, (e, f) => new { e, f })
                    .Join(_context.Brands, h => h.f.id_brand, i => i.id, (h, i) => new { h, i })
                    .Where(p => p.h.e.c.a.id == id && p.h.e.c.a.active == true)
                    .Select(p => new Brands
                    {
                        id_department = p.i.id_department
                    }).FirstOrDefault().id_department;
        }
        [HttpGet("TakeAttributeItem/{idItem}")]
        public IEnumerable<PostViewModel> TakeAttributeItem(int idItem)
        {
            var items = _context.Items
                    .Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Join(_context.Categories, e => e.c.a.id_category, f => f.id, (e, f) => new { e, f })
                    .Join(_context.Brands, h => h.f.id_brand, i => i.id, (h, i) => new { h, i })
                    .Where(g => g.h.e.c.a.id == idItem && g.h.e.c.a.active == true)
                    .Select(p => new PostViewModel
                    {
                        item = p.h.e.c.a,
                        size = p.h.e.c.b,
                        dsSize = p.h.e.d,
                        brand_name = p.i.name,
                        cate_name = p.h.f.name,
                    }).ToList();

            return items;
        }

        [HttpGet("Detail/{idItem}/{dsSize}")]
        public IEnumerable<PostViewModel> Detail(int idItem, int dsSize)
        {
            var items = _context.Items
                    .Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Join(_context.Categories, e => e.c.a.id_category, f => f.id, (e, f) => new { e, f })
                    .Join(_context.Brands, h => h.f.id_brand, i => i.id, (h, i) => new { h, i })
                    .Where(g => g.h.e.c.a.id == idItem && g.h.e.d.VN == dsSize && g.h.e.c.a.active == true)
                    .Select(p => new PostViewModel
                    {
                        item = p.h.e.c.a,
                        size = p.h.e.c.b,
                        dsSize = p.h.e.d,
                        brand_name = p.i.name,
                        cate_name = p.h.f.name,
                    }).ToList();

            return items;
        }

        [Route("ProductDetail")]
        public IActionResult ProductDetail()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var items = _context.Items;

            if (items == null)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [Route("TakeLowestAskMinMin")]
        public IActionResult TakeLowestAskMinMin()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lowest_ask_min = _context.Items.Select(p => p.lowest_ask).Min();

            return Ok(lowest_ask_min);
        }

        [Route("TakeLowestAskMinMax")]
        public IActionResult TakeLowestAskMinMax()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lowest_ask_max = _context.Items.Select(p => p.lowest_ask).Max();

            return Ok(lowest_ask_max);
        }
        [Route("GetItemByIdDepart/{id}")]
        public IEnumerable<Items> GetItemByIdDepart(int id)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(p => p.d.id_department == id && p.c.a.active == true)
                .Select(p => p.c.a);
        }

        [Route("Popular/{id_depart}")]
        public IEnumerable<Items> GetItemsPopular(int id_depart)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.active == true && k.d.id_department == id_depart && k.c.a.active == true && k.c.a.lowest_ask != null)
                .OrderBy(h => h.c.a.sold)
                .Take(10)
                .Select(m => m.c.a);
        }
        [Route("LowestAskNew/{id_depart}")]
        public IEnumerable<Items> GetItemslowestask(int id_depart)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.active == true && k.d.id_department == id_depart && k.c.a.active == true && k.c.a.lowest_ask != null)
                .OrderBy(h => h.c.a.lowest_ask)
                .Take(10)
                .Select(m => m.c.a);
        }

        [Route("TakeItemById_cate/{id}")]
        public IEnumerable<Items> TakeItemById_cate(int id)
        {
            int a = _context.Items.Where(b => b.id == id).First().id_category;
            return _context.Items
               .Where(b => b.id_category == a && b.id != id).Take(5);
        }

        [Route("HighestBidNew/{id_depart}")]
        public IEnumerable<Items> GetItemshighestbid(int id_depart)
        {
            return _context.Items
                .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(k => k.c.a.active == true && k.d.id_department == id_depart && k.c.a.active == true && k.c.a.highest_bid != null)
                .OrderBy(h => h.c.a.highest_bid)
                .Take(10)
                .Select(m => m.c.a);
        }

        [HttpPost]
        [Route("TakeIdCategory_Checkbox")]
        public IEnumerable<Items> TakeIdCategory_Checkbox([FromBody] JObject json)
        {
            dynamic data = json;
            JArray cate = data.cate;
            JArray size = data.size;
            int value_brand = data.id_brand;

            List<int> value_cate = cate.ToObject<List<int>>();
            List<int> value_size = size.ToObject<List<int>>();

            //Mới vào xổ ra hết Items
            var result = _context.Items.AsQueryable();

            if (value_brand != 0)
            {
                result = result.Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Where(x => x.d.id == value_brand && x.c.a.active == true)
                .Select(p => p.c.a);
            }

            if (value_cate.Count != 0)
            {
                result = result.Where(x => value_cate.Contains(x.id_category) && x.active == true).OrderBy(x => x.name);
            }

            if (value_size.Count != 0)
            {
                result = result.Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Where(x => value_size.Contains(x.d.id) && x.c.a.active == true)
                    .Select(p => p.c.a);
            }

            return result.ToList();
        }

        [Route("BuyBid")]
        public IActionResult Buy([FromBody] CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Account == "")
            {
                return BadRequest("Vui Lòng Nhập Tên Người Nhận");
            }
            if (model.Phone == "")
            {
                return BadRequest("Vui Lòng Số Điện Thoại");
            }
            if (model.Address == "")
            {
                return BadRequest("Vui Lòng Địa Chỉ");
            }

            return Ok(model);
        }
    }
}