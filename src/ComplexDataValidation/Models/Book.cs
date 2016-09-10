using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person Person { get; set; }
        public bool Submited { get; set; }

        public Information Information { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}