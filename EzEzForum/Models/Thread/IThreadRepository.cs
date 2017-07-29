using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public interface IThreadRepository {
        IEnumerable<Thread> Threads { get; }
    }
}
