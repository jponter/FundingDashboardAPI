using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundingDashboardAPI.Pages
{
    [Authorize(Roles = "Manager")]
    public class ListProfModel : PageModel
    {
        private readonly AppDbContext db = null;
        public List<Professions> Professions { get; set; }
        public ListProfModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {

            //this.Professions = (from p in db.Professions orderby p.ProfessionId select p).ToList();

        }
    }
}
