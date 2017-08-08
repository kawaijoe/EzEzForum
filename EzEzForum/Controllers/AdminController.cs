using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzEzForum.Models;
using EzEzForum.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EzEzForum.Controllers {
    public class AdminController : Controller {

        public SSDDatabaseContext db = new SSDDatabaseContext();

        public IActionResult Index() {
            return new RedirectResult("../Home");
        }

        public IActionResult ViewUser() {
            if (!Authentication.isAdmin(HttpContext))
                return new RedirectResult("Index");

            List<int> bannedMember = db.BannedMember.Select(x => x.MemberId).ToList();

            List<Member> list = db.Member
                    .Where(b => !bannedMember.Contains(b.MemberId))
                    .ToList();
            list.Reverse();
            return View(list);
        }

        public IActionResult ViewBannedUser() {
            if (!Authentication.isAdmin(HttpContext))
                return new RedirectResult("Index");

            List<int> bannedMember = db.BannedMember.Select(x => x.MemberId).ToList();

            List<Member> list = db.Member
                    .Where(b => bannedMember.Contains(b.MemberId))
                    .ToList();
            list.Reverse();
            return View(list);
        }

        public IActionResult ViewReportedUser() {
            if (!Authentication.isAdmin(HttpContext))
                return new RedirectResult("Index");

            List<int> bannedMember = db.BannedMember.Select(x => x.MemberId).ToList();

            List<ReportedMember> list = db.ReportedMember
                    .Where(b => !bannedMember.Contains(b.MemberId))
                    .Include("Member")
                    .ToList();
            list.Reverse();
            return View(list);
        }

        public IActionResult BanUser(bool hasfailed) {
            ViewBag.HttpContext = HttpContext;
            if (!Authentication.isAdmin(HttpContext))
                return new RedirectResult("Index");

            ViewBag.failed = hasfailed;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BanUserPost(string User, string Reason) {
            ViewBag.HttpContext = HttpContext;

            bool memberBanned = false;

            Member member = db.Member
                .Where(b => b.Email == User)
                .FirstOrDefault();

            if (member != null) {
                foreach (var bannedMember in db.BannedMember.ToList()) {
                    if (bannedMember.MemberId == member.MemberId) {
                        memberBanned = true;
                    }
                }
            }

            if (member != null && !memberBanned) {

                db.BannedMember.Add(new BannedMember {
                    ReasonForBan = Reason,
                    MemberId = getMemberId(User),
                    BanBy = getEmail(Authentication.getMemberId(HttpContext))
                });
                db.SaveChanges();

                return new RedirectResult("ViewBannedUser");
            }

            return new RedirectResult("BanUser?hasFailed=true");
        }

        public string getEmail(int memberId) {
            return db.Member.Where(b => b.MemberId == memberId)
                .FirstOrDefault().Email;
        }

        public int getMemberId(string email) {
            return db.Member.Where(b => b.Email == email)
                .FirstOrDefault().MemberId;
        }



    }
}