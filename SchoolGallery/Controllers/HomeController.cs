using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolGallery.DAL;
using SchoolGallery.Models;
using SchoolGallery.Models.ViewModels;

namespace SchoolGallery.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;
        public HomeController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Category(int? id)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.SelectID = id ??0;
            homeViewModel.Items = _context.Category.ToList();
            return View(homeViewModel);
        }
        public IActionResult CategoryInfo(int id)
        {

            return View(_context.Content.Where(z=>z.CategoryID==id).ToList());
        }
        public IActionResult Detail(int id)
        {
           var content= _context.Content.Find(id);
            if (content == null)
                return NotFound();

            return View(content);
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
