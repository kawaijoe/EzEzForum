using System;
using System.Collections.Generic;

namespace EzEzForum.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public int? ThreadId { get; set; }
        public string TagName { get; set; }

        public virtual Thread Thread { get; set; }
    }
}
