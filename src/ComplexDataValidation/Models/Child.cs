using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Child
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int ParentID { get; set; }
        public Parent Parent { get; set; }
    }
}
