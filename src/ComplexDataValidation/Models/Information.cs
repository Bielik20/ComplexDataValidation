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
        [Key, ForeignKey("Book")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } //BookId
        public bool Submited { get; set; }

        public string Titile { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        public Book Book{ get; set; }
    }
}
