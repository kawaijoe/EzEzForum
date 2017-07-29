using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class Member {
        [Key]
        public int MemberID { get; set; }

        public string Role { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime DateJoined { get; set; }

        public List<Thread> Threads { get; set; }
    }
}
