using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Models
{
    public class Book : IComparable<Book>
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

        /// <summary>
        /// Sorts by Date ascending, nulls are at the end of list.
        /// </summary>
        public int CompareTo(Book other)
        {
            if (other.Information == null)
            {
                return -1;
            }
            if (Information == null || Information.CreationDate >= other.Information.CreationDate)
            {
                return 1;
            }
            return -1;
        }
    }
}