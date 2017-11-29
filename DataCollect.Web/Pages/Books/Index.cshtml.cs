using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataCollect.Model;
using DataCollect.Web.Data;

namespace DataCollect.Web.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly DataCollect.Web.Data.ApplicationDbContext _context;

        public IndexModel(DataCollect.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; }

        public async Task OnGetAsync()
        {
            Book = await _context.Book.ToListAsync();
        }
    }
}
