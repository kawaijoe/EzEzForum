using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models.ViewModels {
    public class ThreadsListViewModel {
        public IEnumerable<Thread> Threads { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
