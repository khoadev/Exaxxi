using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Newtonsoft.Json.Linq;

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
        [HttpGet]
        public IEnumerable<Items> GetItems()
        {
            return _context.Items;
        }

        [Route("TakeItemByIdBrand/{Id_Brand}/{Qty}")]
        public IEnumerable<Items> GetAllItemByIdBrand(int Id_Brand, string Qty)
        {
            if (Qty == "all")
            {
                return _context.Items
                    .Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                    .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                    .Where(g => g.d.id == Id_Brand)
                    .Select(p => p.c.a);
            }
            else
            {
                return null;
            }
        }
        
        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Items>> GetItems(int id)
        {
            var items = await _context.Items.FindAsync(id);

            if (items == null)
            {
                return NotFound();
            }

            return items;
        }

        [HttpGet("TakeAttributeItem/{idItem}")]
        public IEnumerable<PostViewModel> TakeAttributeItem(int idItem)
        {
            var items = _context.Items
                    .Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Join(_context.Categories, e => e.c.a.id_category, f => f.id, (e, f) => new { e, f })
                    .Join(_context.Brands, h => h.f.id_brand, i => i.id, (h, i) => new { h, i })
                    .Where(g => g.h.e.c.a.id == idItem)
                    .Select(p => new PostViewModel
                    {
                        item = p.h.e.c.a,
                        size = p.h.e.d.VN,
                        brand_name = p.i.name
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


        [HttpPost]
        [Route("TakeIdCategory_Checkbox")]
        public IEnumerable<Items> TakeIdCategory_Checkbox([FromBody] JObject json)
        {
            dynamic data = json;
            JArray cate = data.cate;
            JArray size = data.size;
            int value_dep = data.id_dep;

            List<int> value_cate = cate.ToObject<List<int>>();
            List<int> value_size = size.ToObject<List<int>>();            

            //Mới vào xổ ra hết Items
            var result = _context.Items.AsQueryable();

            if(value_dep != 0)
            {
                result = result.Join(_context.Categories, a => a.id_category, b => b.id, (a, b) => new { a, b })
                .Join(_context.Brands, c => c.b.id_brand, d => d.id, (c, d) => new { c, d })
                .Join(_context.Departments, e => e.d.id_department, f => f.id, (e, f) => new { e, f })
                .Where(x => x.f.id == value_dep)
                .Select(p => p.e.c.a);
            }

            if(value_cate.Count != 0)
            {
                result = result.Where(x => value_cate.Contains(x.id_category)).OrderBy(x => x.name);
            }

            if(value_size.Count != 0)
            {
                result = result.Join(_context.Sizes, a => a.id, b => b.id_item, (a, b) => new { a, b })
                    .Join(_context.ds_Size, c => c.b.id_ds_size, d => d.id, (c, d) => new { c, d })
                    .Where(x => value_size.Contains(x.d.id))
                    .Select(p => p.c.a);
            }

            return result.ToList();
        }
        
        
    }
}