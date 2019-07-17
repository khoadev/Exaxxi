using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Exaxxi.Models;
using Exaxxi.Helper;

namespace Exaxxi.Controllers
{
    public class UserController : Controller
    {
        //helper
        CallAPI _api = new CallAPI();

        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Buying()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Selling()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Setting()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult Following()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View();
        }

        public IActionResult Edit_Per_Info()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idUser").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

    }
}