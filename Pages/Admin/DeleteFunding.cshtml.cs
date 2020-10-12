using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundingDashboardAPI.Pages.Admin
{
    [Authorize(Policy = "FundingAdmin")]
    public class DeleteFundingModel : PageModel
    {
        private readonly IFundingRepository _fundingRepo;

        [BindProperty]
        public Models.Funding funding { get; set; }

        public string Message { get; set; }

        public DateTime datetime { get; set; }

        public DeleteFundingModel(IFundingRepository fundingRepo)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int fundingId)
        {
            if (fundingId == null)
            {
                return NotFound();
            }

            bool complete = await _fundingRepo.Delete(fundingId);

            if (complete == true)
            {
                TempData["Message"] = "Funding Deleted";
                return RedirectToPage("./Index");
            }
            else
            {
                Message = "Error";
                return Page();
            }

            
        }


    }

}
