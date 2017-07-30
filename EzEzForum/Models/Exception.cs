using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class Exception
    {
        public int ExceptionId { get; set; }
        public int MemberId { get; set; }
        public string ExDesc { get; set; }

        public virtual Member Member { get; set; }
    }
}
