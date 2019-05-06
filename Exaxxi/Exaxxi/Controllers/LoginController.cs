using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exaxxi.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ExaxxiDbContext _exx;

        public LoginController(ExaxxiDbContext ex)
        {
            _exx = ex;
        }

        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                //KT username/Pass có trong DB?
                //KhachHang kh = ctx.KhachHang.SingleOrDefault(p => p.MaKh == model.Username 
                //&& p.MatKhau == model.Password);
                Users user = _exx.Users.SingleOrDefault(p => p.email == model.Email);

                if (user != null)
                {
                    string matkhauHash = (model.Password).ToSHA512();
                    if (user.password != matkhauHash)
                    {
                        ModelState.AddModelError("Lỗi", "Sai mật khẩu");
                        return View();
                    }

                    //ghi nhận đăng nhập thành công
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, user.email),
                        new Claim(ClaimTypes.Name, user.username),
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
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Profile()
        {
            Users users = _exx.Users.SingleOrDefault(p=>p.username == User.Identity.Name);
            if(users != null)
            {
                return View(users);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Register(Users user)
        {
            user.password = (user.password).ToSHA512();
            user.active = true;
            user.level_seller = 1;
            user.score_buyer = 0;
            user.date_registion = DateTime.Now;

            _exx.Add(user);
            _exx.SaveChanges();

            return View("Login");
        }
    }
}