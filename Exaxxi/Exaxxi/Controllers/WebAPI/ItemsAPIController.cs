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
        public async Task<IActionResult> GetItems([FromRoute] int id)
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

            List<int> value_cate = cate.ToObject<List<int>>();
            List<int> value_size = size.ToObject<List<int>>();

            //Mới vào xổ ra hết Items
            var result = _context.Items.AsQueryable();

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