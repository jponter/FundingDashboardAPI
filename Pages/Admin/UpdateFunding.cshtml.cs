using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundingDashboardAPI.Pages.Admin
{
    public class UpdateFundingModel : PageModel
    {

        private readonly IFundingRepository _fundingRepo;

        [BindProperty]
        public Funding funding { get; set; }

        public string Message { get; set; }
        public bool DataFound { get; set; } = true;

        public UpdateFundingModel(IFundingRepository fundingRepo)
        {
            _fundingRepo = fundingRepo;
        }

        public async Task<IActionResult> OnGetAsync(int fundingId)
        {
            this.funding = await _fundingRepo.SelectById(fundingId);
            
            if (funding == null)
            {
                this.DataFound = false;
                this.Message = "Funding Not Found";
                
            }
            else
            {
                this.DataFound = true;
                this.Message = "";
                
            }

            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {

                
                bool success = await _fundingRepo.Update(funding);
                    if (success)
                    {
                        Message = "Funding Updated Successfully";
                    }
                    else
                    {
                        Message = "Update failed! ";
                    }
                
                }
                catch (DbUpdateException ex1)
                {
                    Message = ex1.Message;
                    if (ex1.InnerException != null)
                    {
                        Message += " : " + ex1.InnerException.Message;
                    }

                }
                catch (Exception ex2)
                {
                    Message = ex2.Message;
                }
            }

            return Page();
        }
    }
}
