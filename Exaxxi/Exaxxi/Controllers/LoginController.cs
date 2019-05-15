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
            return RedirectToAction("Index");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View("Login");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult PasswordForget()
        {
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult RenewPassword (string email, string hash)
        {
            ViewBag.Email = email;
            ViewBag.Hash = hash;
            return View();
        }
    }
}