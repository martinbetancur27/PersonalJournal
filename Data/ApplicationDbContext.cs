using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Comment> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Journal>(journal =>
            {
                journal.ToTable("Journal");
                journal.HasKey(p => p.IdJournal);
                journal.Property(p => p.IdJournal).ValueGeneratedOnAdd();
                journal.Property(p => p.Title).IsRequired();
                journal.Property(p => p.CreateDate).IsRequired();
                journal.Property(p => p.LastEditDate).IsRequired(false);
                journal.Property(p => p.Message).IsRequired(false);
                journal.HasOne(p => p.AspNetUsers).WithMany(u => u.Journals).HasForeignKey(p => p.IdUser);
            });

            modelBuilder.Entity<Note>(note =>
            {
                note.ToTable("Note");
                note.HasKey(p => p.IdNote);
                note.Property(p => p.IdNote).ValueGeneratedOnAdd();
                note.Property(p => p.Title).IsRequired();
                note.Property(p => p.CreateDate).IsRequired();
                note.Property(p => p.LastEditDate).IsRequired(false);
                note.Property(p => p.Message).IsRequired();
                note.HasOne(p => p.Journal).WithMany(j => j.Notes).HasForeignKey(p => p.IdJournal);
            });

            modelBuilder.Entity<Comment>(comment =>
            {
                comment.ToTable("Comment");
                comment.HasKey(p => p.IdComment);
                comment.Property(p => p.IdComment).ValueGeneratedOnAdd();
                comment.Property(p => p.CreateDate).IsRequired();
                comment.Property(p => p.Message).IsRequired();
                comment.HasOne(p => p.Note).WithMany(n => n.Comments).HasForeignKey(p => p.IdNote);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
