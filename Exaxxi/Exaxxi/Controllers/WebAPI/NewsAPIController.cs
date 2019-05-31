using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using System.IO;
using Exaxxi.ViewModels;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public NewsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/News
        [HttpGet]
        public IEnumerable<News> GetNews()
        {
            return _context.News.Include("department").Include("admin");
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNews([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = await _context.News.FindAsync(id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [Route("GetNewsByDepart/{id_depart}")]
        public IEnumerable<NewsViewModel> GetNewsByDepart(int id_depart)
        {
            return _context.News.Include(n => n.admin).Include(n => n.department).Where(p => p.id_department == id_depart).OrderBy(p => p.date_create)
                .Select(n => new NewsViewModel {
                    news = n,
                    name_admin = n.admin.name,
                    en_name_depart = n.department.en_name,
                    vi_name_depart = n.department.vi_name
            });
        }
        // GET: api/News/GetNewsDetail/5
        [HttpGet("GetNewsDetail/{id}")]
        public async Task<IActionResult> GetNewsDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = await _context.News.Include(c => c.department).Include(p => p.admin)
                .FirstOrDefaultAsync(m => m.id == id);

            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);

        }
        
        [HttpPost]
        // POST: api/News/PostCreateNews
        [Route("PostCreateNews")]
        public async Task<IActionResult> PostCreateNews([FromBody] News news)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return Ok();
        }

       
    }
}