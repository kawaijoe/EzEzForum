using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class Member
    {
        public Member()
        {
            BannedMemberBanByNavigation = new HashSet<BannedMember>();
            BannedMemberMember = new HashSet<BannedMember>();
            Exception = new HashSet<Exception>();
            Message = new HashSet<Message>();
            ReportedMemberMember = new HashSet<ReportedMember>();
            ReportedMemberReportedByNavigation = new HashSet<ReportedMember>();
            Thread = new HashSet<Thread>();
        }

        public int MemberId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public DateTime DateJoined { get; set; }

        public virtual ICollection<BannedMember> BannedMemberBanByNavigation { get; set; }
        public virtual ICollection<BannedMember> BannedMemberMember { get; set; }
        public virtual ICollection<Exception> Exception { get; set; }
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<ReportedMember> ReportedMemberMember { get; set; }
        public virtual ICollection<ReportedMember> ReportedMemberReportedByNavigation { get; set; }
        public virtual ICollection<Thread> Thread { get; set; }
    }
}
