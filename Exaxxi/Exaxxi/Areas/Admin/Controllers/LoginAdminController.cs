using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Helper;
using Exaxxi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    
    public class LoginAdminController : Controller
    {
        private readonly ExaxxiDbContext _exx;

        //helper
        CallAPI _api = new CallAPI();

        public LoginAdminController(ExaxxiDbContext ex)
        {
            _exx = ex;
        }
   
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpGet, AllowAnonymous]
        [Route("Admin")]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Profile()
        {
            Users users = _exx.Users.SingleOrDefault(p => p.name == User.Identity.Name);
            if (users != null)
            {
                return View(users);
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
    }
}