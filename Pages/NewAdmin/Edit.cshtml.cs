﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FundingDashboardAPI.Models;

namespace FundingDashboardAPI.Pages.NewAdmin
{
    public class EditModel : PageModel
    {
        private readonly FundingDashboardAPI.Models.AppDbContext _context;

        public EditModel(FundingDashboardAPI.Models.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Funding Funding { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Funding = await _context.Funding.FirstOrDefaultAsync(m => m.Id == id);

            if (Funding == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Funding).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundingExists(Funding.Id))
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

        private bool FundingExists(int id)
        {
            return _context.Funding.Any(e => e.Id == id);
        }
    }
}