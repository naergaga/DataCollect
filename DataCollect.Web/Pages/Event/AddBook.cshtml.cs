using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataCollect.Service.Service;
using DataCollect.Model;
using DataCollect.Web.Data;

namespace DataCollect.Web.Pages.Event
{
    public class AddBookModel : PageModel
    {
        public List<Book> BookList { get; set; }
        public CollectEvent Event { get; set; }
        public List<int> BookIdList { get; set; }

        public void OnGet(
            int id,
            [FromServices]ApplicationDbContext context,
            [FromServices]EventService eventService,
            [FromServices]BookService bookService)
        {
            Event = context.Event.SingleOrDefault(t => t.Id == id);
            BookList = bookService.GetList();
            BookIdList = eventService.GetBookIdList(id);
        }

        public async Task<IActionResult> OnPostAsync(int id, List<int> books,
            [FromServices]ApplicationDbContext context,
            [FromServices]EventService eventService)
        {
            var bookIdList = eventService.GetBookIdList(id);

            //remove
            var booksRemove = bookIdList.Except(books);
            foreach (var bookId in booksRemove)
            {
                var item = context.EventBook.Single(t => t.BookId == bookId && t.EventId == id);
                context.EventBook.Remove(item);
            }
            //add
            var booksAdd = books.Except(bookIdList);

            foreach (var bookId in booksAdd)
            {
                context.EventBook.Add(new EventBook { BookId = bookId, EventId = id });
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Item","Event",new { id});
        }
    }
}