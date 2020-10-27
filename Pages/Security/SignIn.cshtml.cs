using FundingDashboardAPI.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Models
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public SignIn SignInData { get; set; }

        private readonly SignInManager<AppIdentityUser> signinManager;


        public SignInModel(SignInManager<AppIdentityUser> signinManager
            )
        {
            this.signinManager = signinManager;
        }



        public void OnGet()
        {

        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await signinManager.PasswordSignInAsync
                (SignInData.UserName, SignInData.Password,
                    SignInData.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }
            }
            return Page();
        }

    }
}