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
        private readonly ExaxxiDbContext _exx;

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
    }
}