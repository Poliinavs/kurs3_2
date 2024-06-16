using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Lab7.Pages.Auth
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
            if (Request.Query.TryGetValue("shortcut", out var shortcutValues))
            {
                if (shortcutValues == "ctrl 1")
                {
                    return Page();
                }
            }
            return RedirectToPage("/Error");
        }

        [BindProperty]
        public string Password { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (_context.Links == null || Password == null)
            {
                return Page();
            }

            if (Password == "1")
            {
                HttpContext.Session.Set("isAdmin", Encoding.UTF8.GetBytes("true"));
            }

            return Redirect("http://localhost:5108/");
        }
    }
}
