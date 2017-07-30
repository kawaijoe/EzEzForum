using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class Thread
    {

        public Thread()
        {
            Message = new HashSet<Message>();
            Tag = new HashSet<Tag>();
            Member = new Member();
        }

        public int ThreadId { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public int? ThreadHits { get; set; }
        public int? MemberId { get; set; }

        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Tag> Tag { get; set; }
        public virtual Member Member { get; set; }
    }
}
