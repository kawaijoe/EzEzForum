using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EzEzForum.Models;
using EzEzForum.Models.ViewModels;

namespace EzEzForum.Controllers {
    public class HomeController : Controller {

        private IMemberRepository memberRepository;
        private IThreadRepository threadRepository;

        public int PageSize = 10;

        public HomeController(IMemberRepository memberRepository, IThreadRepository threadRepository) {
            this.memberRepository = memberRepository;
            this.threadRepository = threadRepository;
        }

        /*
        public IActionResult Index() {
            int page = 1;
            PagingInfo = new PagingInfo {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = threadRepository.Threads.Count()
            }
           return View(threadRepository.Threads
               .OrderBy(p => p.DateTimeCreated)
               .Skip((page - 1) * PageSize)
               .Take(PageSize));
        }
        */

        public IActionResult Index(int page = 1)
            => View(new ThreadsListViewModel {
                Threads = threadRepository.Threads
                    .OrderByDescending(p => p.DateTimeCreated)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = threadRepository.Threads.Count()
                }
            });

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
