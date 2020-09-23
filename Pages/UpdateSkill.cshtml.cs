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
    public class UpdateSkillModel : PageModel
    {

        private readonly AppDbContext db = null;

        [BindProperty]
        public Skills Skill { get; set; }

        public string Message { get; set; }
        public bool DataFound { get; set; } = true;

        public UpdateSkillModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet(int SkillId)
        {
            Skill = db.Skills.Find(SkillId);

            if (Skill == null)
            {
                this.DataFound = false;
                this.Message = "Skill Not Found!";

            }
            else
            {
                this.DataFound = true;
                this.Message = "";
            }

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Skills.Update(Skill);
                    db.SaveChanges();
                    Message = "Skill Updated Succesfully";
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
        }

    }
}


