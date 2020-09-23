using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FundingDashboardAPI.Models;
using FundingDashboardAPI.Pages;
using FundingDashboardAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FundingDashboardAPI.Pages
{
    //[Authorize(Roles = "Manager")]
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public Register RegisterData { get; set; }

        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;

        public RegisterModel(UserManager<AppIdentityUser> userManager,
            RoleManager<AppIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (!await roleManager.RoleExistsAsync("Manager"))
                {
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can perform CRUD operations";
                    IdentityResult roleResult = await roleManager.CreateAsync(role);
                }

                AppIdentityUser user = new AppIdentityUser();
                user.UserName = RegisterData.UserName;
                user.Email = RegisterData.Email;
                user.FullName = RegisterData.FullName;
                user.BirthDate = RegisterData.BirthDate;

                IdentityResult result = await userManager.CreateAsync(user, RegisterData.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                    return RedirectToPage("/Security/SignIn");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User Details!");
                }
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
