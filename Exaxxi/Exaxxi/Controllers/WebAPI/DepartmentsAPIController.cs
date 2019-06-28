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
    [Route("api/DepartmentsAPI")]
    [ApiController]
    public class DepartmentsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public DepartmentsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public IEnumerable<Departments> GetDepartments()
        {
            return _context.Departments.OrderBy(p=>p.order);
        }
        [HttpGet("DepartHasNews")]
        public IEnumerable<Departments> DepartHasNews()
        {
            return _context.Departments
                .Join(_context.News, a => a.id, b => b.id_department, (a, b) => new { a, b })
                .Select(p => p.a).Distinct()
                ;
        }
        [Route("Take1ByOrder")]
        public Departments Get1Departments()
        {
            return _context.Departments.OrderBy(p => p.order).FirstOrDefault();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departments>> GetDepartments([FromRoute] int id)
        {

            var departments = await _context.Departments.FindAsync(id);

            if (departments == null)
            {
                return NotFound();
            }

            return departments;
        }
    }
}