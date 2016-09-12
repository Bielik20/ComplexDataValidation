using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Information
    {
        [Key, ForeignKey("Person")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } //BookId
        public bool Submited { get; set; }

        public string Titile { get; set; }
        public DateTime CreationDate { get; set; }

        public Person Person { get; set; }
    }
}
