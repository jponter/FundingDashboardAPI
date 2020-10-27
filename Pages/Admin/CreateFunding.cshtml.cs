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
    public class CreateFundingModel : PageModel
    {
        private readonly IFundingRepository _fundingRepo;

        [BindProperty]
        public Funding funding { get; set; }

        public string Message { get; set; }

        public DateTime datetime { get; set; }

        public CreateFundingModel(IFundingRepository fundingRepo)
        {
            _fundingRepo = fundingRepo;
        }

        public IActionResult OnGet()
        {
            datetime = DateTime.Now;



            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {

            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //string dt = DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
            funding.AddedOn = DateTime.Now;
            funding.AddedBy = User.Identity.Name;

            if (ModelState.IsValid)
            {


                try
                {


                    bool success = await _fundingRepo.Insert(funding);
                    if (success)
                    {
                        TempData["Message"] = "Funding Created Successfully";
                        return RedirectToPage("./Index");
                    }
                    else
                    {
                        Message = "Create failed! ";
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
