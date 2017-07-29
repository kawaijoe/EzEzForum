using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EzEzForum.Models {
    public class ApplicationDbContext : DbContext {

        public DbSet<Member> Member { get; set; }
        public DbSet<Thread> Thread { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Thread>()
                .HasOne(p => p.Member)
                .WithMany(b => b.Threads)
                .HasForeignKey(i => i.MemberID);

            // thread = post
            base.OnModelCreating(modelBuilder);
        }

    }
}
