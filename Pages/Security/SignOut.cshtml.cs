using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FundingDashboardAPI.Models;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Authorization;
using FundingDashboardAPI.Security;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FundingDashboardAPI.Pages
{
    [Authorize]
    public class SignOutModel : PageModel
    {
        private readonly SignInManager<AppIdentityUser> signinManager;
        //private readonly ILogger<SignOutModel> _logger;
        private IOptions<OidcOptions> options;
        public bool isAdmin = false;
        public SignOutModel(SignInManager<AppIdentityUser> signinManager, IOptions<OidcOptions> options)
        {
            this.signinManager = signinManager;
            this.options = options;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LogoutUser(User.FindFirstValue("onelogin-access-token"));
            await HttpContext.SignOutAsync();
            return RedirectToPage("/Security/Signin");
        }

        private async Task<bool> LogoutUser(string accessToken)
        {
            using (var client = new HttpClient())
            {

                // The Token Endpoint Authentication Method must be set to POST if you
                // want to send the client_secret in the POST body.
                // If Token Endpoint Authentication Method then client_secret must be
                // combined with client_id and provided as a base64 encoded string
                // in a basic authorization header.
                // e.g. Authorization: basic <base64 encoded ("client_id:client_secret")>
                var formData = new FormUrlEncodedContent(new[]
                {
              new KeyValuePair<string, string>("token", accessToken),
              new KeyValuePair<string, string>("token_type_hint", "access_token"),
              new KeyValuePair<string, string>("client_id", options.Value.ClientId),
              new KeyValuePair<string, string>("client_secret", options.Value.ClientSecret)
          });

                var uri = String.Format("https://{0}.onelogin.com/oidc/token/revocation", options.Value.Region);

                var res = await client.PostAsync(uri, formData);

                return res.IsSuccessStatusCode;
            }
        }
    }
}