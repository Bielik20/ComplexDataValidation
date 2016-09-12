using ComplexDataValidation.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Pet
    {
        [Key, ForeignKey("Person")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } //PersonId
        public bool Submited { get; set; }

        [Required]
        public string Name { get; set; }
        public KindEnum Kind { get; set; }

        public Person Person { get; set; }
    }
}