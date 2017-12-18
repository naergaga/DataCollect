using DataCollect.Model;
using DataCollect.Web.Data;
using DataCollect.Web.Services.Service;
using DataCollect.Web.Utities.Option;
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
        private ColumnService columnService;
        private SheetService _sheetService;

        public BookService(ApplicationDbContext context, ColumnService columnService, SheetService sheetService)
        {
            _context = context;
            this.columnService = columnService;
            _sheetService = sheetService;
        }

        public void Add(Book book)
        {
            _context.Book.Add(book);
            _context.SaveChanges();
        }

        /// <summary>
        /// 获取book结构
        /// </summary>
        public Book Get(int id)
        {
            var book = _context.Book.Include(b => b.Sheets).SingleOrDefault(t => t.Id == id);

            FillSheets(book);
            return book;
        }

        public Book GetWithData(int id)
        {
            var book = _context.Book.Include(b => b.Sheets).SingleOrDefault(t => t.Id == id);

            FillSheetsData(book);
            return book;
        }

        /// <summary>
        /// 获取book结构
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Book Get(string name)
        {
            var book = _context.Book.Include(b => b.Sheets).SingleOrDefault(t => t.Name == name);

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
                sheet.Columns = columnService.GetOrderList(sheet.Id);
            });
        }

        /// <summary>
        /// 填充列名，数据
        /// </summary>
        /// <param name="book"></param>
        public void FillSheetsData(Book book, string userId)
        {
            book.Sheets.ForEach(sheet =>
            {
                _sheetService.FillRows(sheet, userId);
                _sheetService.FillCols(sheet);
            });
        }

        public void FillSheetsData(Book book)
        {
            book.Sheets.ForEach(sheet =>
            {
                _sheetService.FillRows(sheet);
                _sheetService.FillCols(sheet);
            });
        }

        public void FillSheetsData(Book book, PageOption option)
        {
            book.Sheets.ForEach(sheet =>
            {
                _sheetService.FillRows(sheet,option);
                _sheetService.FillCols(sheet);
            });
        }

        public void FillSheetsData(Book book, string userId, PageOption option)
        {
            var maxCount = option.Count;
            book.Sheets.ForEach(sheet =>
            {
                _sheetService.FillRows(sheet,userId, option);
                _sheetService.FillCols(sheet);

                if (option.Count > maxCount) { maxCount = option.Count; }
            });
            option.Count = maxCount;
        }
    }
}
