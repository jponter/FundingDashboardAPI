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
    public class DeleteProfModel : PageModel
    {

        private readonly AppDbContext db = null;

        [BindProperty]
        public Professions Profession { get; set; }

        public string Message { get; set; }
        public bool DataFound { get; set; } = true;

        public DeleteProfModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet(int ProfessionId)
        {
            Profession = db.Professions.Find(ProfessionId);

            if (Profession == null)
            {
                this.DataFound = false;
                this.Message = "Profession Not Found!";

            }
            else
            {
                this.DataFound = true;
                this.Message = "";
            }

        }

        public IActionResult OnPost()
        {
            Professions prof = db.Professions.Find(Profession.ProfessionId);
            try
            {
                db.Professions.Remove(prof);
                db.SaveChanges();
                TempData["Message"] = "Profession Deleted Succesfully";
                return RedirectToPage("/ListProf");
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
