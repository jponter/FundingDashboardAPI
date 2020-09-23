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
    public class ListSkillsModel : PageModel
    {
        private readonly AppDbContext db = null;

        public List<Skills> Skills { get; set; }
        public int professionID = 0;

        public ListSkillsModel(AppDbContext db)
        {
            this.db = db;
        }


        public void OnGet(int professionID)
        {
            this.Skills = (from s in db.Skills where s.ProfessionId == professionID orderby s.SkillId select s).ToList();
            this.professionID = professionID;
        }
    }
}
