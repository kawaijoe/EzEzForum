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
using System.Diagnostics;

namespace EzEzForum.Controllers {
    public class HomeController : Controller {

        public int PageSize = 10;
        public SSDDatabaseContext db = new SSDDatabaseContext();

        public HomeController() { }

        public IActionResult Index(string search) {
            ViewBag.HttpContext = HttpContext;

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

        public IActionResult ViewThread(int id) {
            ViewBag.HttpContext = HttpContext;

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
                ViewBag.MemberId = Authentication.getMemberId(HttpContext);
            }

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewThreadPost(Message message) {
            ViewBag.HttpContext = HttpContext;

            if (message.MemberId != null && message.Msg != null) {
                Debug.WriteLine("bebud");
                message.DateTimeCreated = DateTime.Now;
                db.Message.Add(message);
                db.SaveChanges();
            }

            return new RedirectResult("ViewThread?id=" + message.ThreadId);
            //return RedirectToAction("ViewThread?id=" + message.ThreadId, "Home");
        }

        public IActionResult Login(bool hasfailed) {
            ViewBag.HttpContext = HttpContext;
            if (Authentication.isLoggedon(HttpContext))
                return new RedirectResult("Index");

            ViewBag.failed = hasfailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(string User, string Pass) {
            ViewBag.HttpContext = HttpContext;

            bool memberBanned = false;

            Member member = db.Member
                .Where(b => b.Email == User)
                .FirstOrDefault();

            foreach (var bannedMember in db.BannedMember.ToList()) {
                if (bannedMember.MemberId == member.MemberId) {
                    memberBanned = true;
                }
            }

            if (member != null && member.Pass == Pass && !memberBanned) {
                Authentication.login(HttpContext, member);
                return new RedirectResult("Index");
            }

            return new RedirectResult("Login?hasFailed=true");
        }

        public IActionResult Logout() {
            ViewBag.HttpContext = HttpContext;
            if (Authentication.isLoggedon(HttpContext))
                Authentication.logout(HttpContext);

            return new RedirectResult("Index");
        }

        public IActionResult Register(bool hasfailed) {
            ViewBag.HttpContext = HttpContext;
            if (Authentication.isLoggedon(HttpContext))
                return new RedirectResult("Index");
            ViewBag.failed = hasfailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(string User, string Pass, string ConfirmPass) {
            ViewBag.HttpContext = HttpContext;
            if (User != null && Pass != null && ConfirmPass != null && Pass == ConfirmPass && User.Contains("@") && emailExist(User)) {

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

        private bool emailExist(string email) {
            Member member = db.Member.Where(b => b.Email == email)
                .FirstOrDefault();
            if (member == null) return true;
            return false;
        }

        public IActionResult ViewProfile(int id) {
            ViewBag.HttpContext = HttpContext;

            if (id == 0) {
                id = Authentication.getMemberId(HttpContext);
            }

            Member member = db.Member
                .Where(b => b.MemberId == id)
                .FirstOrDefault();

            ViewBag.Email = member.Email;
            ViewBag.DateJoined = member.DateJoined;

            // Auth
            ViewBag.auth = false;
            if (Authentication.isLoggedon(HttpContext)) {
                Member memberReportedBy = db.Member
                    .Where(b => b.MemberId == Authentication.getMemberId(HttpContext))
                    .FirstOrDefault();

                ViewBag.auth = true;
                ViewBag.ReportedBy = memberReportedBy.Email;
                ViewBag.MemberId = Authentication.getMemberId(HttpContext);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewProfilePost(ReportedMember report) {
            ViewBag.HttpContext = HttpContext;
            Debug.WriteLine("bebud");
            report.DateTimeReported = DateTime.Now;
            db.ReportedMember.Add(report);
            db.SaveChanges();

            return new RedirectResult("Index");
            //return RedirectToAction("ViewThread?id=" + message.ThreadId, "Home");
        }

        public IActionResult Error() {
            return View();
        }
    }
}
