﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab7.Data;
using Lab7.Models;

namespace Lab7.Pages.Link
{
    public class EditModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public EditModel(Lab7.Data.AppDbContext context)
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

            var link =  await _context.Links.FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }
            Link = link;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Update(Link);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(Link.Id))
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

        private bool LinkExists(int id)
        {
          return (_context.Links?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
