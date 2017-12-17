using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    public class CollectEvent
    {
        public int Id { get; set; }
        [DisplayName("名称")]
        public string Name { get; set; }
        [DefaultValue(false)]
        [DisplayName("发布")]
        public bool Published { get; set; }

        public virtual List<Book> Books { get; set; }
    }

    public class EventBook
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }

        public virtual Book Book { get; set; }
        public virtual CollectEvent Event { get; set; }
    }
}
