using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundingDashboardAPI.Pages.Admin
{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {

        private readonly IFundingRepository _fundingRepo;

        public List<Funding> Funding { get; set; }

       public IndexModel(IFundingRepository fundingRepo)
        {
            _fundingRepo = fundingRepo;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            this.Funding = await _fundingRepo.SelectAll();
            return Page();
        }
    }
}
