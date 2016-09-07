using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Person
    {
        public int ID { get; set; }

        //Required
        public Credentials Credentials { get; set; }
        //Optional
        public Pet Pet { get; set; }
        //Required
        public List<Book> Books { get; set; }
    }
}