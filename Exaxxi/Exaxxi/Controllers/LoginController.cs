using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Exaxxi.Common;
using Exaxxi.Helper;
using Exaxxi.Models;
using Exaxxi.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

namespace Exaxxi.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ExaxxiDbContext _exx;

        //helper
        CallAPI _api = new CallAPI();

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

        public IActionResult Profile()
        {
<<<<<<< HEAD
            Users users = _exx.Users.FirstOrDefault(p=>p.name == User.Identity.Name);
            if(users != null)
=======
            Users users = _exx.Users.SingleOrDefault(p => p.name == User.Identity.Name);
            if (users != null)
>>>>>>> b01acf05b7759096f65ef772a088d8e46d58b45a
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
            return View("Login");
        }
    }
}