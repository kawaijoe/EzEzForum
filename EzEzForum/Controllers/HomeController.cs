using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzEzForum.Models;
using EzEzForum.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EzEzForum.Controllers {
    public class HomeController : Controller {

        public int PageSize = 10;
        public SSDDatabaseContext db = new SSDDatabaseContext();

        public HomeController() { }

        public IActionResult Index() {
            return View(db.Thread.Include("Member"));
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() {
            return View();
        }
    }
}
