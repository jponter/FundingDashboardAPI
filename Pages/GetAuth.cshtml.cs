using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FundingDashboardAPI.Pages
{
    [Authorize(Policy = "Cloudreach")]
    public class GetAuthModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/wwwsecure/index.html");
        }
    }
}
