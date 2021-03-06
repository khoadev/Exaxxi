﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exaxxi.Controllers.WebAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public DepartmentsChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartments([FromRoute] int id, [FromBody] Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departments.id)
            {
                return BadRequest();
            }

            _context.Entry(departments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<IActionResult> PostDepartments([FromBody] Departments departments)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //API Add
            _context.Departments.Add(departments);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartments", new { id = departments.id }, departments);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartments([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var departments = await _context.Departments.FindAsync(id);
            if (departments == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(departments);
            await _context.SaveChangesAsync();

            return Ok(departments);
        }
        [HttpGet("DepartmentsExists/{id}")]
        private bool DepartmentsExists(int id)
        {
            return _context.Departments.Any(e => e.id == id);
        }
    }
}