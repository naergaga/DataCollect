using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataCollect.Model;
using DataCollect.Web.Data;

namespace DataCollect.Web.Pages.Event
{
    public class DeleteModel : PageModel
    {
        private readonly DataCollect.Web.Data.ApplicationDbContext _context;

        public DeleteModel(DataCollect.Web.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CollectEvent CollectEvent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CollectEvent = await _context.Event.SingleOrDefaultAsync(m => m.Id == id);

            if (CollectEvent == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CollectEvent = await _context.Event.FindAsync(id);

            if (CollectEvent != null)
            {
                _context.Event.Remove(CollectEvent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
