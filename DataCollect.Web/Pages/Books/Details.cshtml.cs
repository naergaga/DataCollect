using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataCollect.Model;
using DataCollect.Web.Data;
using DataCollect.Service.Service;

namespace DataCollect.Web.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly DataCollect.Web.Data.ApplicationDbContext _context;

        public DetailsModel(DataCollect.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Book Book { get; set; }

        public ActionResult OnGet(int? id,
            [FromServices]BookService service)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = service.Get((int)id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
