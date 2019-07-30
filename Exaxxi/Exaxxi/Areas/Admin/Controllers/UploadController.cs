using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Exaxxi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UploadController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public IActionResult UploadCKEditor(IFormFile upload)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads", fileName);
            var stream = new FileStream(path, FileMode.Create);
            upload.CopyToAsync(stream);
           // return RedirectToAction(nameof(FileBrowse)); 
            return new JsonResult(new { path = "/uploads/" + fileName });
        }
        
        [HttpGet]
        public IActionResult FileBrowse()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads"));
            ViewBag.fileInfos = dir.GetFiles();
            return View("FileBrowse");
        }
 
        [HttpPost]
        public IActionResult CreateFolder(string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads");
            string pathString = System.IO.Path.Combine(path, name);
            System.IO.Directory.CreateDirectory(pathString);
            pathString = System.IO.Path.Combine(pathString, name);
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {
                  
                   
                }
               
            }
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads"));
            ViewBag.fileInfos = dir.GetFiles();
            return View("FileBrowse");

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public void CreateFolder2(string name)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads");
            string pathString = System.IO.Path.Combine(path, name);
            System.IO.Directory.CreateDirectory(pathString);
            pathString = System.IO.Path.Combine(pathString, name);
            if (!System.IO.File.Exists(pathString))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(pathString))
                {


                }

            }
            //var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), hostingEnvironment.WebRootPath, "uploads"));
            //ViewBag.fileInfos = dir.GetFiles();
            
        }
    }
}