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
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using Newtonsoft;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        BlowFish bf = new BlowFish(info.keyBF);

        public UsersAPIController(ExaxxiDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<Users> GetUsers()
        {
            return _context.Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
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

            return Ok(users);
        }

        [AllowAnonymous, Route("CheckExistMail")]
        public IActionResult Check(string email)
        {
            Users user = _context.Users.SingleOrDefault(p => p.email == email);
            if (user == null)
            {
                return Ok("Bạn có thể sử dụng email này");
            }
            else
            {
                return Ok("Email này đã tồn tại");
            }
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

        [AllowAnonymous ,Route("PostUserLogin")]
        public async Task<IActionResult> PostUserByEmail([FromBody] LoginViewModel model, string returnUrl = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = _context.Users.SingleOrDefault(p => p.email == model.Username);
            if (user == null)
            {
                return NotFound("khong tim thay du lieu");
            }
            
            if (bf.Decrypt_CBC(user.password) != model.Password)
            {
                //ModelState.AddModelError();
                return BadRequest("Sai mật khẩu");
            }


            //ghi nhận đăng nhập thành công
            var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, user.email),
                        new Claim(ClaimTypes.Name, user.name),
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
                return RedirectToAction("Profile", "Login");//default
            }
        }

        [AllowAnonymous ,Route("PostRegister")]
        public IActionResult PostRegister([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                        
            //Validate Google recaptcha below
            if (!GoogleRecaptchaHelper.IsReCaptchaPassedAsync(model.captcha, "6Lfwz6IUAAAAAM_gyYa0tzAoeyVYKZ5rkOxT_d6h"))
            {
                return BadRequest("Vui Lòng Nhập Captcha");
            }

            model.password = bf.Encrypt_CBC(model.password);
            Users account = model.toUsers();

            _context.Add(account);
            _context.SaveChanges();

            //mailer
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Exaxxi Site", "tdd3107973@gmail.com"));
            message.To.Add(new MailboxAddress("Hihi", model.email));
            message.Subject = "Register in Exaxxi";
            message.Body = new TextPart("plain")
            {
                Text = "Chúc mừng bạn đã đăng ký thành công!"
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("tdd3107973@gmail.com", "serqltuuwlbddnhb");
                client.Send(message);
                client.Disconnect(true);
            }

            return Ok(model);
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