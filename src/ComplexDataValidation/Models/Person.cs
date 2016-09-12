using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        //Required
        public Credentials Credentials { get; set; }
        //Optional
        public Pet Pet { get; set; }
        //Required
        public virtual List<Book> Books { get; set; }
    }
}