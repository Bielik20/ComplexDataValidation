using ComplexDataValidation.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Pet
    {
        public int ID { get; set; }
        public int ParentID { get; set; }

        public string Name { get; set; }
        public KindEnum Kind { get; set; }
    }
}
