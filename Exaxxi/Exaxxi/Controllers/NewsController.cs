using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exaxxi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Exaxxi.Helper;
using Exaxxi.ViewModels;
using PagedList.Core;
using Exaxxi.Common;
using AutoMapper;

namespace Exaxxi.Controllers
{
    public class NewsController : Controller
    {
        CallAPI _api = new CallAPI();
        private readonly IMapper mapper;

        public NewsController(IMapper _map)
        {
            mapper = _map;
        }

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

        public IActionResult NewsDep(int? loai, int page = 1)
        {
            ViewBag.Id_Dep = loai;

            var dsNews = JsonConvert.DeserializeObject<List<NewsViewModel>>(_api.getAPI("api/NewsAPI/GetNewsByDepart/" + loai).Result);

            var data = new List<NewsViewModel>();
            NewsViewModel new_temp = null;
            foreach (var news in dsNews.ToList())
            {
                new_temp = mapper.Map<NewsViewModel>(news);
                data.Add(new_temp);
            }

            PagedList<NewsViewModel> model = new PagedList<NewsViewModel>(dsNews.AsQueryable(), page, info.PAGE_SIZE);

            return View(model);
        }
    }
}