using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundingDashboardAPI.Pages.Admin
{
    public class DetailFundingModel : PageModel
    {
        private readonly IFundingRepository _fundingRepo;

        [BindProperty]
        public Models.Funding funding { get; set; }

        public string Message { get; set; }
        public string dt { get; set; }

        public DateTime datetime { get; set; }

        public DetailFundingModel(IFundingRepository fundingRepo)
        {
            _fundingRepo = fundingRepo;
        }
        public async Task<IActionResult> OnGetAsync(int fundingId)
        {
            if (fundingId == null)
            {
                return NotFound();
            }

            funding = await _fundingRepo.SelectById(fundingId);

            if (funding == null)
            {
                return NotFound();
            }

            dt = funding.AddedOn.ToString("yyyy-MM-dd HH:mm:ss");
            return Page();
        }
    }
}
