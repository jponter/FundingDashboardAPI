using Microsoft.AspNetCore.Identity;

namespace FundingDashboardAPI.Security
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
