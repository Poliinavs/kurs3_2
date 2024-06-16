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
    public class DetailsModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public DetailsModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

      public Links Link { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Links == null)
            {
                return NotFound();
            }

            var link = await _context.Links.Include(x => x.Comments).FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> OnPostAsync(string handler, int id)
        {
            switch (handler)
            {
                case "OnPlus":
                    return await OnPlus(id);
                case "OnMinus":
                    return await OnMinus(id);
                default:
                    return RedirectToPage("./Index");
            }
        }

        public async Task<IActionResult> OnPlus(int id)
        {
            var linkDb = await _context.Links.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (linkDb == null)
            {
                return Page();
            }
            linkDb.Plus++;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnMinus(int id)
        {
            var linkDb = await _context.Links.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (linkDb == null)
            {
                return Page();
            }
            linkDb.Minus++;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
