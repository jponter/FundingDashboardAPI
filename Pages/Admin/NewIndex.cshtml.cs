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
    [Authorize(Roles = "Manager")]
    public class NewIndexModel : PageModel
    {
        private readonly AppDbContext db = null;

        public string CSPSort { get; set; }
        public string DateSort  { get; set; }

        public NewIndexModel(AppDbContext db)
        {
            this.db = db;
        }
        private readonly IFundingRepository _fundingRepo;

        

        public List<Funding> Funding { get; set; }

       //public NewIndexModel(IFundingRepository fundingRepo)
       // {
       //     _fundingRepo = fundingRepo;
       // }


        public async Task<IActionResult> OnGetAsync(string sortOrder)
        {
            //this.Funding = await _fundingRepo.SelectAll();
            //return Page();

            CSPSort = String.IsNullOrEmpty(sortOrder) ? "csp_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Funding> fundingIQ = from f in db.Funding
                                            select f;

            switch (sortOrder)
            {
                case "csp_desc":
                    fundingIQ = fundingIQ.OrderByDescending(f => f.CSP);
                    break;
                case "Date":
                    fundingIQ = fundingIQ.OrderBy(f => f.AddedOn);
                    break;
                case "date_desc":
                    fundingIQ = fundingIQ.OrderByDescending(f => f.AddedOn);
                    break;
                default:
                    fundingIQ = fundingIQ.OrderBy(f => f.Id);
                    break;
            }

            Funding = await fundingIQ.AsNoTracking().ToListAsync();

            return Page();
        }
    }
}
