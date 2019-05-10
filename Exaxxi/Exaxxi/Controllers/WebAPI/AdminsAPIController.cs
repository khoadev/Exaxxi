using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Exaxxi.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;

        public AdminsAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public IEnumerable<Admins> GetAdmins()
        {
            return _context.Admins;
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdmins([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admins = await _context.Admins.FindAsync(id);

            if (admins == null)
            {
                return NotFound();
            }

            return Ok(admins);
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmins([FromRoute] int id, [FromBody] Admins admins)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != admins.id)
            {
                return BadRequest();
            }

            _context.Entry(admins).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminsExists(id))
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

        // POST: api/Admins
        [HttpPost]
        public async Task<IActionResult> PostAdmins([FromBody] Admins admins)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
            _context.Admins.Add(admins);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdmins", new { id = admins.id }, admins);
        }
        [HttpPost("PostAdminsEmail")]
        public async Task<IActionResult> PostAdminByEmail([FromBody] LoginViewModel model, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Admins admin = _context.Admins.SingleOrDefault(p => p.email == model.Username);
            if (admin == null)
            {
                return NotFound("khong tim thay du lieu");
            }

            string matkhauHash = (model.Password).ToSHA512();
            if (admin.password != matkhauHash)
            {
                //ModelState.AddModelError();
                return BadRequest("Sai mật khẩu");
            }

            //ghi nhận đăng nhập thành công
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, admin.email),
                        new Claim(ClaimTypes.Name, admin.name),
                    };

            // create identity
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal claimsPricipal = new ClaimsPrincipal(userIdentity);

            await HttpContext.SignInAsync(claimsPricipal);

            //Lấy lại trang yêu cầu (nếu có)
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Login");//default
            }
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmins([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var admins = await _context.Admins.FindAsync(id);
            if (admins == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admins);
            await _context.SaveChangesAsync();

            return Ok(admins);
        }

        private bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.id == id);
        }
    }
}