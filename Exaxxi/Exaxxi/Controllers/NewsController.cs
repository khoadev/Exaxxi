using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Exaxxi.Helper;
using Exaxxi.ViewModels;

namespace Exaxxi.Controllers
{
    public class NewsController : Controller
    {
        CallAPI _api = new CallAPI();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            NewsViewModel rs = JsonConvert.DeserializeObject<NewsViewModel>(_api.getAPI("api/NewsAPI/GetById/" + id).Result);

           
            ViewBag.Id_new = id;
            return View(rs);
        }
    }
}