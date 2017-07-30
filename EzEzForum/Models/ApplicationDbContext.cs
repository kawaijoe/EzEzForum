using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class ApplicationDbContext : DbContext {

        //public DbSet<Member> Member { get; set; }
        //public DbSet<Thread> Thread { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
