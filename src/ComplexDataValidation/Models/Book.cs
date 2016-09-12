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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public bool Submited { get; set; }

        public Information Information { get; set; }
        public List<Chapter> Chapters { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
    }
}