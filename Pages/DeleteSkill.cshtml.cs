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
    public class DeleteSkillModel : PageModel
    {
        private readonly AppDbContext db = null;

        [BindProperty]
        public Skills Skill { get; set; }

        public string Message { get; set; }
        public bool DataFound { get; set; } = true;

        public DeleteSkillModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet(int skillId)
        {
            Skill = db.Skills.Find(skillId);

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

        public IActionResult OnPost()
        {
            int Professions = Skill.ProfessionId;
            Skills skilltorem = db.Skills.Find(Skill.SkillId);
            try
            {
                db.Skills.Remove(skilltorem);
                db.SaveChanges();
                TempData["Message"] = "Skill Deleted Succesfully";
                return RedirectToPage("/ListSkills", new
                {
                    professionId = Professions
                });
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
            return Page();
        }

    }
}

