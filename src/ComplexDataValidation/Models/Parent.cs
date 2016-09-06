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

        public List<Child> Children { get; set; }
    }
}
