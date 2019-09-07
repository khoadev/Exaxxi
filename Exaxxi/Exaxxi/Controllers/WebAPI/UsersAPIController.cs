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
using System.Text.RegularExpressions;

namespace Exaxxi.Controllers.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersAPIController : ControllerBase
    {
        private readonly ExaxxiDbContext _context;
        BlowFish bf = new BlowFish(info.keyBF);
        Mailer ml = new Mailer();

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
        public IActionResult GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _context.Users.Include(u => u.service_fee).Where(r => r.id == id).FirstOrDefault();

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
                return Ok("<span style='color:green'>Bạn có thể sử dụng email này</span>");
            }
            else
            {
                return Ok("<span style='color:red'>Email này đã tồn tại</span>");
            }
        }

        [AllowAnonymous, Route("CountUsers")]
        public IActionResult CountUsers()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _context.Users.Count();

            return Ok(users);
        }

        [AllowAnonymous, Route("GetForgetPassword")]
        public IActionResult GetForgetPassword(string email)
        {
            Users user = _context.Users.SingleOrDefault(p => p.email == email);
            if (ModelState.IsValid)
            {
                //Mã hóa email
                var hash = bf.Encrypt_CBC(email);
                var link = $"http://localhost:51340/Login/RenewPassword?email={email}&hash={hash}";

                //mailer
                ml.SendMail("Exaxxi Site", email, "Forgot Password in Exaxxi", $"Vui lòng click vào link {link} để đổi mật khẩu");

                return Ok("ok");
            }

            return Ok(user);
        }



        [AllowAnonymous, Route("PostUserLogin")]
        public IActionResult PostUserByEmail([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Users user = _context.Users.SingleOrDefault(p => p.email == model.Username);
            if (user == null)
            {
                return NotFound("Không tìm thấy dữ liệu");
            }

            if (bf.Decrypt_CBC(user.password) != model.Password)
            {
                return BadRequest("Sai mật khẩu");
            }

            if(user.active == false)
            {
                return BadRequest("Tài khoản không khả dụng. Vui lòng liên hệ Admin để xử lý!");
            }

            //Ghi nhận đăng nhập thành công
            return Ok(user);
        }

        [AllowAnonymous, Route("PostRegister")]
        public IActionResult PostRegister([FromBody] JObject json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dynamic data = json;
            string name = data.name;
            string email = data.email;
            string password = data.password;
            string confirm_password = data.confirm_password;
            string phone = data.phone;
            string address = data.address;
            int id_city = data.id_city;
            int num_item_selled = data.num_item_selled;
            string captcha = data.captcha;
            
            if (password != confirm_password)
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
            //Validate Google recaptcha below
            if (!GoogleRecaptchaHelper.IsReCaptchaPassedAsync(captcha, "6Lfwz6IUAAAAAM_gyYa0tzAoeyVYKZ5rkOxT_d6h"))
            {
                return BadRequest("Vui Lòng Nhập Captcha");
            }

            password = bf.Encrypt_CBC(password);
            Users account = new Users();
            account.name = name;
            account.email = email;
            account.password = password;
            account.phone = phone;
            account.address = address;
            account.id_city = id_city;
            account.num_item_selled = num_item_selled;
            account.level_seller = 1;
            account.score_buyer = 0;
            account.date_registion = DateTime.Now;
            account.active = true;

            _context.Add(account);
            _context.SaveChanges();

            //mailer
            ml.SendMail("Exaxxi Site", email, "Register in Exaxxi", "Chúc mừng bạn đã đăng ký thành công!");

            return Ok();
        }

        [AllowAnonymous, Route("RenewPassword")]
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

                Users user = _context.Users.SingleOrDefault(p => p.email == username);
                user.password = bf.Encrypt_CBC(password);
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return BadRequest("Link sai!");
            }
        }

        [AllowAnonymous, Route("User_Request")]
        public IActionResult User_Request([FromBody] JObject json)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dynamic data = json;            
            string re_pro_cate = data.re_pro_cate;
            string re_pro_name = data.re_pro_name;
            string re_pro_link = data.re_pro_link;
            string re_desc_pro = data.re_desc_pro;
            string email_user = data.email_user;

            if (re_pro_cate.Length == 0)
            {
                return BadRequest("Product's category cannot be null!");
            }
            if (re_pro_name.Length == 0)
            {
                return BadRequest("Product's name cannot be null!");
            }
            if(re_desc_pro.Length == 0)
            {
                return BadRequest("Please describe your product!");
            }            

            //mailer
            ml.SendMail("Exaxxi Site", email_user, "Request Product!", $"Product's category: {re_pro_cate}.\nProduct's name: {re_pro_name}.\nProduct's link: {re_pro_link}.\nDescribe: {re_desc_pro}.");

            return Ok();
        }
    }
}