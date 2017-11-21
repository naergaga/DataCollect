using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    public class Row
    {
        public int Id { get; set; }
        [ForeignKey("Sheet")]
        public int SheetId { get; set; }
        //public string UserId { get; set; }

        public Sheet Sheet { get; set; }
        public List<ColumnData> Data { get; set; }
    }
}
