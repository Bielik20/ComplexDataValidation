using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public int ChildID { get; set; }

        public string Description { get; set; }
    }
}
