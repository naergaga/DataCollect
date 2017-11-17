using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    [Table("Column")]
    public class Column
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("Sheet")]
        public int SheetId { get; set; }

        public Sheet Sheet { get; set; }
    }
}
