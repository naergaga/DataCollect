using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataCollect.Model;
using DataCollect.Web.Data;

namespace DataCollect.Web.Pages.Event
{
    public class EditModel : PageModel
    {
        private readonly DataCollect.Web.Data.ApplicationDbContext _context;

        public EditModel(DataCollect.Web.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CollectEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectEventExists(CollectEvent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CollectEventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
