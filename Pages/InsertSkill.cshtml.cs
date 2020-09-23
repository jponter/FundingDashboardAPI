using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FundingDashboardAPI.Pages
{
    [Authorize(Roles = "Manager")]
    public class InsertSkillModel : PageModel
    {
        private readonly AppDbContext db = null;

        public string Message { get; set; }
        [BindProperty]
        public Skills Skill { get; set; }
        public int ProfessionId { get; set; }
        public InsertSkillModel(AppDbContext db)
        {
            this.db = db;
        }
        public void OnGet(int professionId)
        {
            this.ProfessionId = professionId;
        }

        public void OnPost(int ProfessionId)
        {
            this.ProfessionId = ProfessionId;
            if (ModelState.IsValid)
            {
                try
                {
                    db.Skills.Add(Skill);
                    db.SaveChanges();
                    Message = "Skill Inserted Successfully";
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
            else
            {
                Message = "Error please alert your admin";
            }


        }
    }
}
