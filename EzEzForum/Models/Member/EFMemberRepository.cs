using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class EFMemberRepository : IMemberRepository {
        private ApplicationDbContext context;

        public EFMemberRepository(ApplicationDbContext context) {
            this.context = context;
        }

        public IEnumerable<Member> Members => context.Member;
    }
}
