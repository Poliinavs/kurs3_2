using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab7.Data;
using Lab7.Models;

namespace Lab7.Pages.Link
{
    public class DeleteModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public DeleteModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Links Link { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FirstOrDefaultAsync(m => m.Id == id);

            if (link == null)
            {
                return NotFound();
            }
            else 
            {
                Link = link;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }
            var link = await _context.Links.FindAsync(id);

            if (link != null)
            {
                Link = link;
                _context.Links.Remove(Link);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
