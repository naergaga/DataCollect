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

        public EventService(ApplicationDbContext context,
            SheetService sheetService)
        {
            _context = context;
            _sheetService = sheetService;
        }

        public void Publish(int id)
        {
            var event1 = _context.Event.SingleOrDefault(t=>t.Id == id);
            event1.Published = true;
            _context.Update(event1);
            _context.SaveChanges();
        }

        public List<CollectEvent> GetList(bool published)
        {
            return _context.Event.Where(t => t.Published==published).ToList();
        }

        public void CanelPublish(int id)
        {
            var event1 = _context.Event.SingleOrDefault(t => t.Id == id);
            event1.Published = false;
            _context.Update(event1);
            _context.SaveChanges();
        }

        public CollectEvent Get(int id)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Id == id);

            FillEvent(collectEvent);
            return collectEvent;
        }

        public CollectEvent Get(string name)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Name == name);

            FillEvent(collectEvent);
            return collectEvent;
        }

        private void FillEvent(CollectEvent collectEvent)
        {
            collectEvent.Books = (from b in _context.Book
                                  join eb in _context.EventBook
                                  on b.Id equals eb.BookId
                                  where eb.EventId == collectEvent.Id
                                  select b).Include(t => t.Sheets).ToList();
            collectEvent.Books.ForEach(book =>
            {
                book.Sheets.ForEach(sheet =>
                {
                    _sheetService.FillRows(sheet);
                    _sheetService.FillCols(sheet);
                });
            });
        }

        public List<CollectEvent> GetList()
        {
            return _context.Event.ToList();
        }
    }
}
