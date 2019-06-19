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
        [HttpGet("GetAll")]
        public IEnumerable<NewsViewModel> GetAll()
        {
            return _context.News.Include(n => n.admin).Include(n => n.department)
              .Select(n => new NewsViewModel
              {
                  news = n,
                  name_admin = n.admin.name,
                  en_name_depart = n.department.en_name,
                  vi_name_depart = n.department.vi_name,
                   id_depart=n.id_department
              });
        }
        [HttpGet("GetById/{id}")]
        public NewsViewModel GetById(int id)
        {
            return _context.News.Include(n => n.admin).Include(n => n.department).Where(p=>p.id==id)
              .Select(n => new NewsViewModel
              {
                  news = n,
                  name_admin = n.admin.name,
                  en_name_depart = n.department.en_name,
                  vi_name_depart = n.department.vi_name,
                  id_depart = n.id_department
              }).FirstOrDefault();
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
        [HttpGet("GetNewsOrderByTime")]
        public IEnumerable<NewsViewModel> GetNewsOrderByTime()
        {
            return _context.News.Include(n => n.admin).Include(n => n.department).OrderByDescending(p => p.date_create).Take(4)
               .Select(n => new NewsViewModel
               {
                   news = n,
                   name_admin = n.admin.name,
                   en_name_depart = n.department.en_name,
                   vi_name_depart = n.department.vi_name
               });
        }
        [HttpGet("GetNewsOrderByView")]
        public IEnumerable<NewsViewModel> GetNewsOrderByView()
        {
            return _context.News.Include(n => n.admin).Include(n => n.department).OrderByDescending(p => p.view).Take(4)
               .Select(n => new NewsViewModel
               {
                   news = n,
                   name_admin = n.admin.name,
                   en_name_depart = n.department.en_name,
                   vi_name_depart = n.department.vi_name
               });
        }

        [Route("GetNewsByDepart/{id_depart}/{id}")]
        public IEnumerable<NewsViewModel> GetNewsByDepart(int id_depart,int id)
        {
            return _context.News.Include(n => n.admin).Include(n => n.department).Where(p => p.id_department == id_depart && p.id!=id).OrderBy(p => p.date_create)
                .Select(n => new NewsViewModel {
                    news = n,
                    name_admin = n.admin.name,
                    en_name_depart = n.department.en_name,
                    vi_name_depart = n.department.vi_name
            });
        }
        [Route("GetNewsByDepart/{id_depart}")]
        public IEnumerable<NewsViewModel> GetNewsByDepart1(int id_depart)
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