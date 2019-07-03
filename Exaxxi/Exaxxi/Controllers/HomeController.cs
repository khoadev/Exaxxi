using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exaxxi.Models;
using Exaxxi.Common;
using Newtonsoft.Json;
using Exaxxi.Helper;

namespace Exaxxi.Controllers
{
    public class HomeController : Controller
    {
        CallAPI _api = new CallAPI();
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                Departments depart = JsonConvert.DeserializeObject<List<Departments>>(_api.getAPI("api/DepartmentsAPI").Result).FirstOrDefault();
                ViewBag.Id_Depart = depart.id;
            }
            else
            {
                ViewBag.Id_Depart = id;
            }
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
