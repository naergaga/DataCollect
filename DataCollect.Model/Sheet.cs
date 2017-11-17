using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    [Table("Sheet")]
    public class Sheet
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }

        public virtual List<Column> Columns { get; set; }
        public virtual Book Book { get; set; }
    }
}
