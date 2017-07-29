using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class StubRepository : IMemberRepository {
        public IEnumerable<Member> Members => new List<Member> {
            new Member { MemberID = 1, Role = "A", Email = "admin@mail.com", Pass = "xxx", DateJoined = DateTime.Now },
            new Member { MemberID = 2, Role = "M", Email = "member@mail.com", Pass = "xxx", DateJoined = DateTime.Now }
        };
    }
}
