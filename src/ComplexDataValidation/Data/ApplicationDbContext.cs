using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComplexDataValidation.Models;

namespace ComplexDataValidation.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Details> Details { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
    }
}
