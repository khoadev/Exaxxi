using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;

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
            return _context.Departments.OrderBy(p=>p.order).ToList();
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