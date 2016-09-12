using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Credentials
    {
        [Key, ForeignKey("Person")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } //PersonId
        public bool Submited { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public Person Person { get; set; }
    }
}