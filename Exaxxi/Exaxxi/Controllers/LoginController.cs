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

        [HttpPost, AllowAnonymous]
        public IActionResult Register(RegisterViewModel user)
        {
            user.password = (user.password).ToSHA512();
            Users account = user.toUsers();

            _exx.Add(account);
            _exx.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EscanorDuyTran", "tdd3107973@gmail.com"));
            message.To.Add(new MailboxAddress("Hihi", "tdd310797@gmail.com"));
            message.Subject = "Register in Exaxxi";
            message.Body = new TextPart("plain")
            {
                Text = "<h3>Ban da dang ky thanh cong</h3>"
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("tdd3107973@gmail.com", "serqltuuwlbddnhb");
                client.Send(message);
                client.Disconnect(true);
            }

            return View("Login");
        }
    }
}