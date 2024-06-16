using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Lab7.Models;

namespace Lab7.Pages.Comment
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
        public Comments Comment { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id)
        {
            HttpContext.Session.Set("CreateComment", Encoding.UTF8.GetBytes("true"));

            var link = _context.Links.Where(x => x.Id == id).FirstOrDefault();
            if (_context.Comments == null || Comment == null || link == null)
            {
                return Page();
            }
            Comment.SessionId = this.HttpContext.Session.Id;
            Comment.Link = link;
            var a = this.HttpContext.Session.Get("isAdmin");
            if (this.HttpContext.Session.Get("isAdmin") == null)
            {
                Comment.Role = "user";
            }
            else
            {
                Comment.Role = "admin";
            }
            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Link/Index");
        }
    }
}
