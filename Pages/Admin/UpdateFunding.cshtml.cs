using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Pages.Admin
{
    [Authorize(Policy = "FundingAdmin")]
    public class UpdateFundingModel : PageModel
    {

        private readonly IFundingRepository _fundingRepo;

        [BindProperty]
        public Funding funding { get; set; }

        public string Message { get; set; }
        public string dt { get; set; }
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
            dt = funding.AddedOn.ToString("yyyy-MM-dd HH:mm:ss");
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //string dt = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            funding.AddedOn = DateTime.Now;


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
