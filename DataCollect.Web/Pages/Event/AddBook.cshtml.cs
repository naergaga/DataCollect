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

        public void OnGet(
            int id,
            [FromServices]ApplicationDbContext context,
            [FromServices]BookService bookService)
        {
            Event = context.Event.SingleOrDefault(t => t.Id == id);
            BookList = bookService.GetList();
        }

        public async Task<IActionResult> OnPostAsync(int id, List<int> books,
            [FromServices]ApplicationDbContext context)
        {
            foreach (var bookId in books)
            {
                context.EventBook.Add(new EventBook { BookId = bookId, EventId = id });
            }
            await context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}