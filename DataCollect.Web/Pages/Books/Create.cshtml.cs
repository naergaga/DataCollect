using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataCollect.Model;
using DataCollect.Web.Data;
using Microsoft.AspNetCore.Http;
using DataCollect.Service.Provider;

namespace DataCollect.Web.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly DataCollect.Web.Data.ApplicationDbContext _context;

        public CreateModel(DataCollect.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile excelfile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var exBook = new BookProvider().Get(excelfile.OpenReadStream());
            Book.Sheets = exBook.Sheets;

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}