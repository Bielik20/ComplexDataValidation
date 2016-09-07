using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Chapter
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public bool Submited { get; set; }

        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}