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


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            
            modelbuilder.Entity<Journal>().ToTable("Journal");
            modelbuilder.Entity<Note>().ToTable("Note");
            modelbuilder.Entity<Comment>().ToTable("Comment");


            //The entity type 'IdentityUserLogin<string>' requires a primary key to be defined.
            base.OnModelCreating(modelbuilder);
        }
    }
}
