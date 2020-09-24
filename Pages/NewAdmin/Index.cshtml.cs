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
    public class IndexModel : PageModel
    {
        private readonly FundingDashboardAPI.Models.AppDbContext _context;

        public IndexModel(FundingDashboardAPI.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Funding> Funding { get;set; }

        public async Task OnGetAsync()
        {
            Funding = await _context.Funding.ToListAsync();
        }
    }
}
