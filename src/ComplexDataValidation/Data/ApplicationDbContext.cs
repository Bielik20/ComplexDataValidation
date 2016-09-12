using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplexDataValidation.Models;
using System.Data.Entity;

namespace ComplexDataValidation.Data
{
    [DbConfigurationType(typeof(CodeConfig))]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
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
