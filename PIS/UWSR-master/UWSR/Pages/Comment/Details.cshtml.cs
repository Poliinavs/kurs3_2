using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lab7.Data;
using Lab7.Models;

namespace Lab7.Pages.Comment
{
    public class DetailsModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public DetailsModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

      public Comments Comment { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Comments == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            else 
            {
                Comment = comment;
            }
            return Page();
        }
    }
}
