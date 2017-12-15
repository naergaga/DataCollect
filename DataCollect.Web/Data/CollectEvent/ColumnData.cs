using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    [Table("ColumnData")]
    public class ColumnData
    {
        public int Id { get; set; }
        [Required]
        public String Value { get; set; }
        [ForeignKey("Column")]
        public int ColumnId { get; set; }
        public int RowId { get; set; }

        public Column Column { get; set; }
        public Row Row { get; set; }
    }
}
