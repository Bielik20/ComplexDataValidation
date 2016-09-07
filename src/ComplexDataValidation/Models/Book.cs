using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Book
    {
        public int ID { get; set; }
        public int PersonID { get; set; }
        public Person Person { get; set; }
        public bool Submited { get; set; }

        public Information Information { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}