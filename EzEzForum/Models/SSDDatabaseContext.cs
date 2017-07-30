using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EzEzForum.Models
{
    public partial class SSDDatabaseContext : DbContext
    {
        public virtual DbSet<BannedMember> BannedMember { get; set; }
        public virtual DbSet<Exception> Exception { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<ReportedMember> ReportedMember { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Thread> Thread { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=139.59.119.31;Initial Catalog=SSDDatabase;User ID=sa;Password=yourStrong(!)Password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BannedMember>(entity =>
            {
                entity.HasKey(e => new { e.BannedId, e.MemberId })
                    .HasName("PK_BannedMembers");

                entity.Property(e => e.BannedId)
                    .HasColumnName("BannedID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.BanBy)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DateTimeBan)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReasonForBan)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                entity.HasOne(d => d.BanByNavigation)
                    .WithMany(p => p.BannedMemberBanByNavigation)
                    .HasPrincipalKey(p => p.Email)
                    .HasForeignKey(d => d.BanBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_BannedMembers_BanBy");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.BannedMemberMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_BannedMembers_MemberID");
            });

            modelBuilder.Entity<Exception>(entity =>
            {
                entity.Property(e => e.ExceptionId).HasColumnName("ExceptionID");

                entity.Property(e => e.ExDesc).HasColumnType("varchar(500)");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Exception)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Exception_MemberID");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Member__A9D10534E8729F10")
                    .IsUnique();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.DateJoined)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.DateTimeCreated).HasColumnType("datetime");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ThreadId).HasColumnName("ThreadID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_TMessage_MemberID");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ThreadId)
                    .HasConstraintName("FK_TMessage_ThreadID");
            });

            modelBuilder.Entity<ReportedMember>(entity =>
            {
                entity.HasKey(e => new { e.ReportedId, e.MemberId })
                    .HasName("PK_ReportedMembers");

                entity.Property(e => e.ReportedId)
                    .HasColumnName("ReportedID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.DateTimeReported)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.ReasonForReport)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.ReportedBy)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.ReportedMemberMember)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReportedMembers_MemberID");

                entity.HasOne(d => d.ReportedByNavigation)
                    .WithMany(p => p.ReportedMemberReportedByNavigation)
                    .HasPrincipalKey(p => p.Email)
                    .HasForeignKey(d => d.ReportedBy)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ReportedMembers_ReportedBy");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ThreadId).HasColumnName("ThreadID");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Tag)
                    .HasForeignKey(d => d.ThreadId)
                    .HasConstraintName("FK_Tag_ThreadID");
            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.Property(e => e.ThreadId).HasColumnName("ThreadID");

                entity.Property(e => e.DateTimeCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("getdate()");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Msg)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ThreadHits).HasDefaultValueSql("0");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Thread)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Threads_CreatedBy");
            });
        }
    }
}