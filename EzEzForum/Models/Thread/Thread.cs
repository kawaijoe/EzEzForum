using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class Thread {
        public int ThreadID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public int ThreadHits { get; set; }
        public int MemberID { get; set; }
    }
}
