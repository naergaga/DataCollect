using DataCollect.Model;
using DataCollect.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Service
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;
        private readonly SheetService _sheetService;


        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CollectEvent Get(int id)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Id == id);


            collectEvent.Books = (from b in _context.Book
                                  join eb in _context.EventBook
                                  on b.Id equals eb.BookId
                                  where eb.EventId == id
                                  select b).Include(t => t.Sheets).ToList();
            collectEvent.Books.ForEach(book =>
            {
                book.Sheets.ForEach(sheet => _sheetService.FillRows(sheet));
            });
            return collectEvent;
        }

        public List<CollectEvent> GetList()
        {
            return _context.Event.ToList();
        }
    }
}
