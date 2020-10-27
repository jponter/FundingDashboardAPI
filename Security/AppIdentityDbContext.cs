using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FundingDashboardAPI.Security
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        // public constructor
        public AppIdentityDbContext
            (DbContextOptions<AppIdentityDbContext> options)
            : base(options)
        {
            // left empty
        }
    }
}
