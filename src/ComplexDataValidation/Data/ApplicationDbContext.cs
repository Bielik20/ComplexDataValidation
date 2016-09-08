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
            //Usefull sites about it:
            // http://stackoverflow.com/questions/7934229/entity-framework-foreign-key-inserts-with-auto-id
            // http://stackoverflow.com/questions/5559043/entity-framework-code-first-two-foreign-keys-from-same-table
            // http://ef.readthedocs.io/en/latest/modeling/relational/fk-constraints.html

            base.OnModelCreating(builder);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
    }
}
