using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab7.Models;

namespace Lab7.Pages.Link
{
    public class CreateModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public CreateModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Links Link { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Link.Plus = 0;
            Link.Minus = 0;
            if (_context.Links == null || Link == null)
            {
                return Page();
            }

            _context.Links.Add(Link);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
