using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FundingDashboardAPI.Models;

namespace FundingDashboardAPI.Pages.NewAdmin
{
    public class DetailsModel : PageModel
    {
        private readonly FundingDashboardAPI.Models.AppDbContext _context;

        public DetailsModel(FundingDashboardAPI.Models.AppDbContext context)
        {
            _context = context;
        }

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
    }
}
