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
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        BlowFish bf = new BlowFish(info.keyBF);

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
        [AllowAnonymous,HttpGet("GetForgetPassword")]
        public IActionResult GetForgetPassword(string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            //mailer
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Exaxxi Site", "tdd3107973@gmail.com"));
            message.To.Add(new MailboxAddress("Hihi", email));
            message.Subject = "Reset password";
            var hash = bf.Encrypt_CBC(email);
            var link = $"http://localhost:51340/Admin/Admins/ChangePassword?email={email}?hash={hash}";
            message.Body = new TextPart("plain")
            {
                Text = $"click vào link {link}"
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("tdd3107973@gmail.com", "serqltuuwlbddnhb");
                client.Send(message);
                client.Disconnect(true);
            }
            return NoContent();

           
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
          
            if (bf.Decrypt_CBC(admin.password) != model.Password)
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

        [AllowAnonymous, HttpPost("RenewPassword")]
        public IActionResult RenewPassword([FromBody] JObject json)
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
                return Ok("dm");
            }
            else
            {
                return BadRequest("sai link");
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