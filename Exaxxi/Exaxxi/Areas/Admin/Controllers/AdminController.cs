using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exaxxi.Controllers.WebAPI;
using Exaxxi.Helper;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]

    public class AdminController : Controller
    {
        private readonly ExaxxiDbContext _context;
        private AdminsAPIController admin;

        //helper
        CallAPI _api = new CallAPI();
        public AdminController (ExaxxiDbContext context)
        {
            _context = context;
            admin = new AdminsAPIController(_context);
        }

        public object ModeState { get; private set; }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet,AllowAnonymous]
        public IActionResult Login(string returnUrl = "zzz")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost,AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = "")
        {
            Admins ad = _context.Admins.SingleOrDefault(p => p.email == model.Username);
            if(ad != null)
            {
                string passwordHash = model.Password;   
                if(ad.password != passwordHash)
                {
                    ModelState.AddModelError("Lỗi", "Mật khẩu sai!");
                    return View();

                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, ad.username),
                    new Claim(ClaimTypes.Email, ad.email),
                };

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                if(Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Admin");
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}