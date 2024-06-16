using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab7.Data;
using Lab7.Models;

namespace Lab7.Pages.Comment
{
    public class EditModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public EditModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comments Comment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment =  await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            Comment = comment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            //_context.Attach(Comment).State = EntityState.Modified;
            _context.Update(Comment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(Comment.Id))
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

        private bool CommentExists(int id)
        {
          return (_context.Comments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
