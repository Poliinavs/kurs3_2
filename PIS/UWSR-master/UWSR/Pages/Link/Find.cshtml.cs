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
    public class FindModel : PageModel
    {
        private readonly Lab7.Data.AppDbContext _context;

        public FindModel(Lab7.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Links> Link { get;set; } = default!;

        [BindProperty]
        public string Search { get; set; } = default;
        public async Task OnGetAsync()
        {
            if (_context.Links != null)
            {
                await SetMessages();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Links != null)
            {
                await SetMessages();
            }
            return Page();
        }

        private async Task SetMessages()
        {
            if(Search != null)
            {
                Link = await _context.Links.Where(x => x.Description.Contains(Search) || x.Url.Contains(Search)).ToListAsync();
            }
            else
            {
                Link = await _context.Links.ToListAsync();
            }
        }
    }
}
