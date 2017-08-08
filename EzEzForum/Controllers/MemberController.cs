using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzEzForum.Models;
using EzEzForum.Infrastructure;

namespace EzEzForum.Views {
    public class MemberController : Controller {

        public SSDDatabaseContext db = new SSDDatabaseContext();

        public IActionResult Index() {
            return new RedirectResult("../Home");
        }

        public IActionResult CreateThread() {
            ViewBag.HttpContext = HttpContext;

            if (!Authentication.isLoggedon(HttpContext))
                return new RedirectResult("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateThreadPost(string Title, string Msg) {
            Member member = db.Member
                .Where(b => b.MemberId == Authentication.getMemberId(HttpContext))
                .FirstOrDefault();

            db.Thread.Add(new Thread() {
                Title = Title,
                Msg = Msg,
                Member = member,
                MemberId = Authentication.getMemberId(HttpContext)
            });
            db.SaveChanges();

            return new RedirectResult("Index");
        }

    }
}