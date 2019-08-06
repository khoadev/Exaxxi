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
using System.Net.Http;
using System.Net.Http.Headers;

namespace Exaxxi.Controllers.WebAPI
{
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
        [AllowAnonymous, HttpGet("GetForgetPassword/{email}")]
        public IActionResult GetForgetPassword(string email)
        {
            if (ModelState.IsValid)
            {
                //Mã hóa email
                var hash = bf.Encrypt_CBC(email);
                var link = $"http://localhost:51340/Admin/Admins/ChangePassword?email={email}&hash={hash}";

                //mailer
                ml.SendMail("Exaxxi Site", email, "Forgot Password in Exaxxi", $"Vui lòng click vào link {link} để đổi mật khẩu");
            }

            return Ok();        
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
            string email = data.email;
            string password = data.password;
            string repass = data.repass;

            //Check Hash                
            if (bf.Decrypt_CBC(hash) == email)
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

                var pass = bf.Encrypt_CBC(password);

                _context.Admins.First(p => p.email == email).password = pass;

                _context.SaveChanges();                
            }
            return Ok();
        }

        [Route("ChangeAdmin")]
        public async Task<IActionResult> ChangeAdmin([FromBody] JObject json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dynamic data = json;
            string pass = data.pass;
            string re_pass = data.re_pass;
            string name = data.name;           
            string email = data.email;           

            if (pass.Length > 0)
            {
                if (pass.Length < 8)
                {
                    return BadRequest("Mật khẩu tối thiểu 8 ký tự!");
                }

                if (pass != re_pass)
                {
                    return BadRequest("Nhập lại mật khẩu chưa chính xác!");
                }

                Regex rgx = new Regex(info.RegEx);
                if (!rgx.IsMatch(pass))
                {
                    return BadRequest("Mật khẩu phải có ký tự Hoa, Số, Thường!");
                }

                var password = bf.Encrypt_CBC(pass);

                _context.Admins.First(p => p.email == email).password = password;
            }

            _context.Admins.First(p => p.email == email).name = name;

            await _context.SaveChangesAsync();

            return Ok();
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
        public HttpClient Initial(string token = null)
        {
            var Client = new HttpClient();

            Client.BaseAddress = new Uri("http://localhost:51340");
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return Client;
        }
    }
}