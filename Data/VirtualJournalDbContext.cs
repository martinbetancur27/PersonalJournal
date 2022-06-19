using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class VirtualJournalDbContext : DbContext
    {
        public VirtualJournalDbContext(DbContextOptions<VirtualJournalDbContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<People> People { get; set; }

        public DbSet<ClientWriter> ClientWriters { get; set; }

        public DbSet<Journal> Journals { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Administrator> Administrators { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Account>().ToTable("Account");
            modelbuilder.Entity<People>().ToTable("People");
            modelbuilder.Entity<ClientWriter>().ToTable("ClientWriter");
            modelbuilder.Entity<Journal>().ToTable("Journal");
            modelbuilder.Entity<Note>().ToTable("Note");
            modelbuilder.Entity<Comment>().ToTable("Comment");
            modelbuilder.Entity<Administrator>().ToTable("Administrator");

            modelbuilder.Entity<Note>()
                .HasOne(e => e.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Note>()
                .HasOne(e => e.Journal)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


            modelbuilder.Entity<Comment>()
                .HasOne(e => e.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<Comment>()
                .HasOne(e => e.Note)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
