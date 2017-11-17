using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
