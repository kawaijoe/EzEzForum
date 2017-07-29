using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class EFThreadRepository : IThreadRepository {
        private ApplicationDbContext context;

        public EFThreadRepository(ApplicationDbContext context) {
            this.context = context;
        }

        public IEnumerable<Thread> Threads => context.Thread;
    }
}
