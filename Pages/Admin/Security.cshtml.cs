using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Pages.Admin
{

    public class SecurityModel : PageModel
    {

        private readonly ILogger<SecurityModel> _logger;
        private IOptions<OidcOptions> options;
        public bool isAdmin = false;

        public SecurityModel(ILogger<SecurityModel> logger, IOptions<OidcOptions> options)
        {
            _logger = logger;
            this.options = options;
        }


        public bool IsAdmin()
        {
            //specific implementation to work with the way OneLogin is setup at Cloudreach - NB: I AM NOT AN ADMIN of that SYSTEM
            String adminGroup = "FundingDashboardAdmin";

            IEnumerable<Claim> groups = User.FindAll("groups");

            var user = User.IsInRole("FundingDashboardAdmin");

            foreach (Claim claim in groups)
            {
                if (claim.Value == adminGroup)
                {
                    // 
                    return true;
                }
            }

            return false;
            //foreach (Claim claim in User.Claims)
            //{
            //    if (claim.Type == "groups")
            //    {
            //        var details = JObject.Parse(claim.Value);
            //        bool isAdmin = (bool)details["jp_funding_admin"];
            //        if (isAdmin)
            //        {
            //            //bool arethey = User.IsInRole("admin");

            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }

            //    }

            //}

        }


        public void OnGet()
        {
            isAdmin = IsAdmin();
            //foreach (var group in User.Claims.Where(x => x.Type == "groups"))
            //{
            //    User.Claims.Append(new Claim(ClaimTypes.Role, group.Value));
            //    User.AddIdentities(new Claim(ClaimTypes.Role, group.Value));
            //}



        }

        public async Task<IActionResult> OnPost()
        {
            //await LogoutUser(User.FindFirstValue("onelogin-access-token"));
            await HttpContext.SignOutAsync();
            //return RedirectToPage("/");
            // raise the logout event


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            SignOut();

            return Redirect("/");



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
