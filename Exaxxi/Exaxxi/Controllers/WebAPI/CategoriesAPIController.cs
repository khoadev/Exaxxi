using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Exaxxi.ViewModels;


namespace Exaxxi.Controllers.WebAPI
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public CategoriesAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public IEnumerable<Categories> GetCategories()
        {            
            return _context.Categories.Include("brand");          
        }
      
        [Route("TakeCateByIdBrand/{Id_Brand}/{Qty}")]
        public IEnumerable<Categories> GetAllCate(int Id_Brand, string Qty)
        {
            if (Qty == "all")
            {
                return _context.Categories.Where(p => p.id_brand == Id_Brand).OrderBy(p => p.order);
            }     
            else
            {
                return null;
            }
        }

        [Route("Take1CateByIdBrand/{Id_Brand}")]
        public Categories Get1Cate(int Id_Brand)
        {
            return _context.Categories.Where(p => p.id_brand == Id_Brand).OrderBy(p => p.order).FirstOrDefault();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategories([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }
        // GET: api/Categories/5
        [HttpGet("GetCategoriesDetail/{id}")]
        public async Task<IActionResult> GetCategoriesDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await _context.Categories.Include(c => c.brand)
                .FirstOrDefaultAsync(m => m.id == id);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }
        [HttpGet("BrowserData")]
        public IEnumerable<BrowserData>  GetDataBrowser()
        {
            return _context.Categories
                    .Join(_context.Brands, a => a.id_brand, b => b.id, (a, b) => new { a, b })
                    .Join(_context.Departments, c => c.b.id_department, d => d.id, (c, d) => new { c, d })
                    .Select(p => new BrowserData
                    {
                        id_brand= p.c.b.id,
                        name_brand=p.c.b.name,
                        id_dep=p.d.id,
                        viname_dep=p.d.vi_name,
                        enname_dep=p.d.en_name,
                        id_cate=p.c.a.id,
                        name_cate=p.c.a.name

                    }
                );
        }
        
    }
}