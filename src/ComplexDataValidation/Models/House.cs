using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class House
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public string City { get; set; }
        public int Size { get; set; }
    }
}
