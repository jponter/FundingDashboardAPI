using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundingDashboardAPI.Pages.Admin
{
    [Authorize(Policy = "FundingAdmin")]
    public class IndexModel : PageModel
    {

        private readonly IFundingRepository _fundingRepo;
        private readonly AppDbContext db = null;
        public PaginatedList<Funding> Fundings { get; set; }


        public List<Funding> Funding { get; set; }

       public IndexModel(IFundingRepository fundingRepo, AppDbContext db)
        {
            _fundingRepo = fundingRepo;
            this.db = db;
        }


        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            int pageSize = 6;
            this.Fundings = await PaginatedList<Funding>.CreateAsync(db.Funding.AsNoTracking(), pageIndex ?? 1, pageSize);
            return Page();
        }
    }
}
