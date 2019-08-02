using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Helper;
using Exaxxi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class OrdersController : Controller
    {
        CallAPI _api = new CallAPI();
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }            
            
            return View();
        }

        public IActionResult Detail(int id, int status, int update)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IdOrder = id;

            //gán
            ViewBag.Status = status;
            ViewBag.Update = update;

            return View();
        }

        public IActionResult Shipping()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public IActionResult History()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetInt32("idAdmin").ToString()))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}
