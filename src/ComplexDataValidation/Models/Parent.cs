using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Parent
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        //Optional
        public Pet Pet { get; set; }
        //Required
        public House House { get; set; }
        //Required
        public List<Child> Children { get; set; }
    }
}
