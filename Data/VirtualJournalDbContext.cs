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
    public class VirtualJournalDbContext : IdentityDbContext<IdentityUser>
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


        //Solution to: Introducing FOREIGN KEY constraint 'FK_Note_Journal_IdJournal' on table 'Note'
        //may cause cycles or multiple cascade paths.
        //Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints.
        //Could not create constraint or index. See previous errors.

            /*modelbuilder.Entity<Note>()
                .HasOne(e => e.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);*/

            modelbuilder.Entity<Note>()
                .HasOne(e => e.Journal)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            /*modelbuilder.Entity<Comment>()
                .HasOne(e => e.Account)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);*/

            modelbuilder.Entity<Comment>()
                .HasOne(e => e.Note)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.
            base.OnModelCreating(modelbuilder);
        }
    }
}
