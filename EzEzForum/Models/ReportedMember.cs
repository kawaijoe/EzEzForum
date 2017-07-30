using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class ReportedMember
    {
        public int ReportedId { get; set; }
        public DateTime DateTimeReported { get; set; }
        public string ReportedBy { get; set; }
        public string ReasonForReport { get; set; }
        public int MemberId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Member ReportedByNavigation { get; set; }
    }
}
