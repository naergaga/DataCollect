using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Model
{
    public class CollectEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
    }

    public class EventBook
    {
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }

        public virtual Book Book { get; set; }
        public virtual CollectEvent Event { get; set; }
    }
}
