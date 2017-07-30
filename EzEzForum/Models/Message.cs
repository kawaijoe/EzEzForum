using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EzEzForum.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        [Required]
        public string Msg { get; set; }
        public DateTime? DateTimeCreated { get; set; }
        [Required]
        public int? MemberId { get; set; }
        [Required]
        public int? ThreadId { get; set; }

        public virtual Member Member { get; set; }
        public virtual Thread Thread { get; set; }
    }
}
