using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class Thread {
        [Key]
        public int ThreadID { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public string Title { get; set; }
        public string Msg { get; set; }
        public int ThreadHits { get; set; }

        
        public int MemberID { get; set; }
        [ForeignKey("MemberID")]
        public Member Member { get; set; }
    }
}
