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
    public class BookService
    {
        private ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        public Book Get(string name)
        {
            var book = _context.Book.Include(b=>b.Sheets).SingleOrDefault(t => t.Name == name);

            FillSheets(book);
            return book;
        }

        public List<Book> GetList()
        {
            return _context.Book.ToList();
        }

        public void FillSheets(Book book)
        {
            book.Sheets.ForEach(sheet =>
            {
                sheet.Columns = _context.Column.Where(c => c.SheetId == sheet.Id).ToList();
            });
        }
    }
}
