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
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Exaxxi.Controllers.WebAPI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class AdminsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        BlowFish bf = new BlowFish(info.keyBF);
        Mailer ml = new Mailer();

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
        [HttpGet("GetDetailAdmins/{id}")]
        public async Task<IActionResult> GetDetailAdmins([FromRoute] int id)
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
        [AllowAnonymous, HttpGet("GetForgetPassword")]
        public IActionResult GetForgetPassword(string email)
        {
            Admins ad = _context.Admins.SingleOrDefault(p => p.email == email);
            if (ModelState.IsValid)
            {
                //Mã hóa email
                var hash = bf.Encrypt_CBC(email);
                var link = $"http://localhost:51340/Admin/Admins/ChangePassword?email={email}&hash={hash}";

                //mailer
                ml.SendMail("Exaxxi Site", email, "Forgot Password in Exaxxi", $"Vui lòng click vào link {link} để đổi mật khẩu");

                return Ok("ok");
            }

            return Ok(ad);


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
        public IActionResult PostAdminByEmail([FromBody] LoginViewModel model, string returnUrl = "")
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

            if (bf.Decrypt_CBC(admin.password) != model.Password)
            {
                //ModelState.AddModelError();
                return BadRequest("Sai mật khẩu");
            }
            //Lưu Session


            HttpContext.Session.SetInt32("idAdmin", admin.id);
            HttpContext.Session.SetString("nameAdmin", admin.name);


            return Ok();
        }

        [AllowAnonymous, Route("ChangePassword")]
        public IActionResult ChangePassword([FromBody] JObject json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dynamic data = json;
            string hash = data.hash;
            string username = data.username;
            string password = data.password;
            string repass = data.repass;

            //Check Hash                
            if (bf.Decrypt_CBC(hash) == username)
            {
                if (password != repass)
                {
                    return BadRequest("Mật khẩu nhập lại không đúng");
                }
                if (password.Length < 8)
                {
                    return BadRequest("Mật khẩu tối thiểu 8 ký tự");
                }
                Regex rgx = new Regex(info.RegEx);
                if (!rgx.IsMatch(password))
                {
                    return BadRequest("Mật khẩu phải có ký tự Hoa, Số, Thường");
                }

                Admins ad = _context.Admins.SingleOrDefault(p => p.email == username);
                ad.password = bf.Encrypt_CBC(password);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest("Link sai!");
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
        [HttpGet("AdminsExists/{id}")]
        public bool AdminsExists(int id)
        {
            return _context.Admins.Any(e => e.id == id);
        }
    }
}