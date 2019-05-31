﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public BrandsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public IEnumerable<Brands> GetBrands()
        {
            return _context.Brands.Where(p=> p.active==true).Select
            (p => new Brands
            {
                id = p.id,
                id_department = p.id_department,
                name = p.name,
            });
        }

        [Route("Take1BrandByIdDepart/{Id_Depart}")]
        public Brands Get1Brands(int Id_Depart)
        {
            return _context.Brands.Where(p => p.id_department == Id_Depart).OrderBy(p => p.order).FirstOrDefault();
        }
        [Route("Take5BrandByIdDepart/{Id_Depart}")]
        public IEnumerable<Brands> GetBrandsByIdDep(int Id_Depart)
        {
            return _context.Brands.Where(p => p.id_department == Id_Depart).OrderBy(p => p.order).Take(5);
        }
        
        [Route("TakeAllBrandByIdDepart/{Id_Depart}")]
        public IEnumerable<Brands> GetAllBrand(int Id_Depart)
        {
            return _context.Brands.Where(p => p.id_department == Id_Depart).OrderBy(p => p.order);
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrands([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brands = await _context.Brands.FindAsync(id);

            if (brands == null)
            {
                return NotFound();
            }

            return Ok(brands);
        }
        // GET: api/Brands/GetbrandsDetail/5
        [HttpGet("GetbrandsDetail/{id}")]
        public async Task<IActionResult> GetbrandsDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await _context.Brands.Include(c => c.department)
                .FirstOrDefaultAsync(m => m.id == id);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }
        
    }
}