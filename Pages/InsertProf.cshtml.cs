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
    public class InsertProfModel : PageModel
    {
        private readonly AppDbContext db = null;

        public string Message { get; set; }

        [BindProperty]
        public Professions Profession { get; set; }

        public InsertProfModel(AppDbContext db)
        {
            this.db = db;
        }

        public void OnGet()
        {
        }


        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Professions.Add(Profession);
                    db.SaveChanges();
                    Message = "Profession Inserted Successfully";
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

