using EzEzForum.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Infrastructure {
    public class Authentication {

        public const string ROLE = "role";
        public const string MEMBERID = "member_id";

        public static void logout(HttpContext httpcontext) {
            httpcontext.Session.Remove(ROLE);
            httpcontext.Session.Remove(MEMBERID);
        }

        public static void login(HttpContext httpcontext, Member member) {
            httpcontext.Session.SetString(ROLE, member.Role);
            httpcontext.Session.SetInt32(MEMBERID, member.MemberId);
        }

        public static int getMemberId(HttpContext httpcontext) {
            return (int)httpcontext.Session.GetInt32(MEMBERID);
        }

        public static bool isUser(HttpContext httpcontext) {
            return httpcontext.Session.GetString(ROLE) == "M";
        }

        public static bool isAdmin(HttpContext httpcontext) {
            return httpcontext.Session.GetString(ROLE) == "A";
        }

        public static bool isLoggedon(HttpContext httpcontext) {
            if (httpcontext.Session.GetString(ROLE) != null) {
                return true;
            }
            return false;
        }
    }
}
