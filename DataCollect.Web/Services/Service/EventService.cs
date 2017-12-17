using DataCollect.Model;
using DataCollect.Web.Data;
using DataCollect.Web.Utities.Option;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataCollect.Service.Service
{
    public class EventService
    {
        private readonly ApplicationDbContext _context;
        private readonly SheetService _sheetService;
        private readonly BookService bookService;

        public EventService(ApplicationDbContext context,
            SheetService sheetService, BookService bookService)
        {
            _context = context;
            _sheetService = sheetService;
            this.bookService = bookService;
        }

        #region GetList
        public List<CollectEvent> GetList()
        {
            return _context.Event.ToList();
        }

        public List<CollectEvent> GetList(bool published)
        {
            return _context.Event.Where(t => t.Published == published).ToList();
        }
        #endregion

        #region publish
        public void CanelPublish(int id)
        {
            var event1 = _context.Event.SingleOrDefault(t => t.Id == id);
            event1.Published = false;
            _context.Update(event1);
            _context.SaveChanges();
        }

        public void Publish(int id)
        {
            var event1 = _context.Event.SingleOrDefault(t => t.Id == id);
            event1.Published = true;
            _context.Update(event1);
            _context.SaveChanges();
        }

        public bool EventPublished(string eventName)
        {
            return _context.Event.Any(t => t.Published && t.Name == eventName);
        }
        #endregion

        #region Get Method

        public CollectEvent Get(string name, string userId)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Name == name);
            FillEvent(collectEvent, userId);
            return collectEvent;
        }

        /// <summary>
        /// Get Full Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CollectEvent Get(int id)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Id == id);

            FillEvent(collectEvent);
            return collectEvent;
        }

        /// <summary>
        /// Get Full Data
        /// </summary>
        public CollectEvent Get(string name)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Name == name);

            FillEvent(collectEvent);
            return collectEvent;
        }

        public CollectEvent Get(int id ,PageOption option)
        {
            var collectEvent = _context.Event.SingleOrDefault(t => t.Id == id);

            FillEvent(collectEvent,option);
            return collectEvent;
        }
        #endregion

        #region FillEvent

        private void FillEvent(CollectEvent collectEvent, PageOption option)
        {
            collectEvent.Books = (from b in _context.Book
                                  join eb in _context.EventBook
                                  on b.Id equals eb.BookId
                                  where eb.EventId == collectEvent.Id
                                  select b).Include(t => t.Sheets).ToList();
            collectEvent.Books.ForEach(book =>
            {
                bookService.FillSheetsData(book,option);
            });
        }

        /// <summary>
        /// 为Event准备所有Book数据
        /// </summary>
        /// <param name="collectEvent"></param>
        private void FillEvent(CollectEvent collectEvent, string userId)
        {
            collectEvent.Books = (from b in _context.Book
                                  join eb in _context.EventBook
                                  on b.Id equals eb.BookId
                                  where eb.EventId == collectEvent.Id
                                  select b).Include(t => t.Sheets).ToList();
            collectEvent.Books.ForEach(book =>
            {
                bookService.FillSheetsData(book, userId);
            });
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
                bookService.FillSheetsData(book);
            });
        }
        #endregion
    }
}
