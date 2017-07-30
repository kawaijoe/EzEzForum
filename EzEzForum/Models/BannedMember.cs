using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class BannedMember
    {
        public int BannedId { get; set; }
        public DateTime DateTimeBan { get; set; }
        public string BanBy { get; set; }
        public string ReasonForBan { get; set; }
        public int MemberId { get; set; }

        public virtual Member BanByNavigation { get; set; }
        public virtual Member Member { get; set; }
    }
}
