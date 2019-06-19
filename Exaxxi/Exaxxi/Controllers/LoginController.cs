﻿using System;
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
using Microsoft.AspNetCore.Http;

namespace Exaxxi.Controllers
{
    
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

       
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public IActionResult Profile()
        {
            //Lấy IdUser đăng nhập
            int? idUser = HttpContext.Session.GetInt32("idUser");

            Users users = _exx.Users.SingleOrDefault(p => p.id == idUser);
            if (users != null)

            {
                return View(users);
            }
            return RedirectToAction("Login");
        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("idUser");
            HttpContext.Session.Remove("nameUser");
            return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View("Login");
        }

        public IActionResult PasswordForget()
        {
            return View();
        }

        public IActionResult RenewPassword (string email, string hash)
        {
            ViewBag.Email = email;
            ViewBag.Hash = hash;
            return View();
        }
    }
}