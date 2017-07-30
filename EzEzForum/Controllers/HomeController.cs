using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzEzForum.Models;
using EzEzForum.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using EzEzForum.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace EzEzForum.Controllers {
    public class HomeController : Controller {

        public int PageSize = 10;
        public SSDDatabaseContext db = new SSDDatabaseContext();

        public HomeController() { }

        public IActionResult Index(string search) {
            List<Thread> list;
            if (search != null)
                list = db.Thread
                    .Where(b => b.Tag.Any(s => search.Contains(s.TagName)))
                    .Include("Member")
                    .Include("Tag")
                    .ToList();
            else
                list = db.Thread
                    .Include("Member")
                    .ToList();
            list.Reverse();
            return View(list);
        }

        public IActionResult CreateThread() {
            return View();
        }

        public IActionResult ViewThread(int id) {
            List<Message> list = db.Message
                .Where(b => b.ThreadId == id)
                .Include("Member")
                .Include("Thread")
                .ToList();
            Thread thread = db.Thread
                .Where(b => b.ThreadId == id)
                .Include("Tag")
                .FirstOrDefault();

            thread.ThreadHits++;
            db.SaveChanges();

            ViewBag.Title = thread.Title;
            ViewBag.DateTimeCreated = thread.DateTimeCreated;
            ViewBag.Msg = thread.Msg;
            ViewBag.ThreadId = thread.ThreadId;
            ViewBag.OPId = thread.Member.MemberId;
            ViewBag.OPEmail = thread.Member.Email;

            // Auth
            ViewBag.auth = false;
            if (Authentication.isLoggedon(HttpContext)) {
                ViewBag.auth = true;
                ViewBag.MemberId = HttpContext.Session.GetString(Authentication.MEMBERID);
            } 

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewThreadPost(Message message) {

            if (message.MemberId != null && message.Msg != null) {
                message.DateTimeCreated = DateTime.Now;
                db.Message.Add(message);
                db.SaveChanges();
            }

            return new RedirectResult("ViewThread?id=" + message.ThreadId);
            //return RedirectToAction("ViewThread?id=" + message.ThreadId, "Home");
        }

        public IActionResult Login(bool hasfailed) {
            if (Authentication.isLoggedon(HttpContext))
                return new RedirectResult("Index");

            ViewBag.failed = hasfailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string User, string Pass) {
            Member member = db.Member
                .Where(b => b.Email == User)
                .FirstOrDefault();

            if (member != null && member.Pass == Pass) {
                Authentication.login(HttpContext, member);
                return new RedirectResult("Index");
            }

            return new RedirectResult("Login?hasFailed=true");
        }

        public IActionResult Logout() {
            if (Authentication.isLoggedon(HttpContext))
                Authentication.logout(HttpContext);

            return new RedirectResult("Index");
        }

        public IActionResult Register(bool hasfailed) {
            if (Authentication.isLoggedon(HttpContext))
                return new RedirectResult("Index");
            ViewBag.failed = hasfailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(string User, string Pass, string ConfirmPass) {

            if (User != null && Pass != null && ConfirmPass != null && Pass == ConfirmPass && User.Contains("@")) {

                db.Member.Add(new Member(){
                    Email = User,
                    Pass = Pass,
                    Role = "M"
                });
                db.SaveChanges();

                return new RedirectResult("Index");
            }

            return new RedirectResult("Register?hasFailed=true");
        }

        public IActionResult ViewProfile() {
            return View();
        }

        public IActionResult Error() {
            return View();
        }
    }
}
