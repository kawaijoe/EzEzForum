using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EzEzForum.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string Msg { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        public int? MemberId { get; set; }
        public int? ThreadId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
