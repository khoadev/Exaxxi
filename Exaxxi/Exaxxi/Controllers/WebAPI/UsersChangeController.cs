using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exaxxi.Common;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersChangeController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        BlowFish bf = new BlowFish(info.keyBF);

        public UsersChangeController(ExaxxiDbContext context)
        {
            _context = context;
        }
        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers([FromRoute] int id, [FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != users.id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUsers([FromBody] Users users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.id }, users);
        }

        [Route("Info_Person")]
        public async Task<IActionResult> Info_Person([FromBody] JObject json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dynamic data = json;
            int id = data.id;
            string name = data.name;
            string email = data.email;
            string pass = data.pass;
            string re_pass = data.re_pass;
            string phone = data.phone;
            string address = data.address;

            var user = _context.Users.Where(p => p.id == id).SingleOrDefault();

            if (pass != "")
            {
                
                if (pass != re_pass)
                {
                    return BadRequest("Nhập lại mật khẩu chưa chính xác!");
                }
                if (pass.Length < 8)
                {
                    return BadRequest("Mật khẩu tối thiểu 8 ký tự!");
                }
                Regex rgx = new Regex(info.RegEx);
                if (!rgx.IsMatch(pass))
                {
                    return BadRequest("Mật khẩu phải có ký tự Hoa, Số, Thường!");
                }
                else
                {
                    user.name = name;
                    user.email = email;
                    user.phone = phone;
                    user.address = address;
                    user.password = bf.Encrypt_CBC(pass);

                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }
            else
            {
                user.name = name;
                user.email = email;
                user.phone = phone;
                user.address = address;

                await _context.SaveChangesAsync();

                return Ok();
            }            
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return Ok(users);
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}