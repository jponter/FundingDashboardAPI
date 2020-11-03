using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace FundingDashboardAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                // TODO: Use your User Agent library of choice here.
                if (BrowserDetection.DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = (SameSiteMode)(-1);
                    //options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {



            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            //Appdb for CareersFramework DB
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            ///appdb for Identity
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("AppDb")));

            //services.AddIdentity<AppIdentityUser, AppIdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();

            // Allow sign in via an OpenId Connect provider like OneLogin
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.LoginPath = "";
                options.AccessDeniedPath = "/security/AccessDenied";
            })
            .AddOpenIdConnect(options =>
            {
                options.ClientId = Configuration["oidc:clientid"];
                options.ClientSecret = Configuration["oidc:clientsecret"];
                //options.Authority = String.Format("https://{0}.onelogin.com/oidc", Configuration["oidc:region"]);
                options.Authority = "https://cloudreach.onelogin.com/oidc/2";




                options.SecurityTokenValidator = new JwtSecurityTokenHandler
                {
                    InboundClaimTypeMap = new Dictionary<string, string>()
                };
                options.ResponseType = OpenIdConnectResponseType.IdToken;
                options.GetClaimsFromUserInfoEndpoint = true;

                //options.Scope.Add("jp_roles");
                options.Scope.Add("groups");


                //options.ClaimActions.MapJsonKey("role", "groups", "role");
                options.TokenValidationParameters.RoleClaimType = "groups";
                options.TokenValidationParameters.NameClaimType = "name";

            }
            );



            services.AddAuthorization(options =>
            {
                options.AddPolicy("FundingAdmin", policy => policy.RequireClaim("groups", "FundingDashboardAdmin"));
                options.AddPolicy("Cloudreach", policy => policy.RequireClaim("groups", "Default"));

                //options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            });

            services.Configure<OidcOptions>(Configuration.GetSection("oidc"));


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Security/SignIn";
                opt.AccessDeniedPath = "/security/AccessDenied";
            });





            //services.AddScoped<IEmployeeRepository, EmployeeSQLRepository>();
            //services.AddScoped<ICountryRespository, CountrySQLRepository>();


            services.AddScoped<IFundingRepository, FundingSqlRepository>();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                //options.Conventions.AddPageRoute("/ListProf", "");
            });

            services.AddControllers();


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/Security/Error", "?code={0}");
            app.UseExceptionHandler("/Security/Error");

            app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");
            app.UseExceptionHandler("/Error");

            app.UseCookiePolicy(); // Before UseAuthentication or anything else that writes cookies.

            //app.UseFileServer();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseStaticFiles(new StaticFileOptions
            {
                //FileProvider = new PhysicalFileProvider(
                //    Path.Combine(env.ContentRootPath, "wwwsecure")),
                //RequestPath = "/wwwsecure"

                OnPrepareResponse = ctx =>
                {
                    if (ctx.Context.Request.Path.StartsWithSegments("/wwwsecure"))
                    {
                        //check for an authenticated user
                        if (!ctx.Context.User.Identity.IsAuthenticated)
                        {
                            // we can send a 401 or do a redirect - here im opting for redirect

                            //// if not HTTP 401
                            //ctx.Context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            //ctx.Context.Response.Headers.Add("Cache-Control", "no-store");

                            ////drop the response
                            //ctx.Context.Response.ContentLength = 0;
                            //ctx.Context.Response.Body = Stream.Null;

                            //send to a page that forces login
                            ctx.Context.Response.Redirect("/GetAuth");
                        }
                    }
                }
            });









            // This is needed if running behind a reverse proxy
            // like ngrok which is great for testing while developing
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                RequireHeaderSymmetry = false,
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }



        public static class BrowserDetection
        {
            // Same as https://devblogs.microsoft.com/aspnet/upcoming-samesite-cookie-changes-in-asp-net-and-asp-net-core/
            public static bool DisallowsSameSiteNone(string userAgent)
            {
                if (string.IsNullOrEmpty(userAgent))
                {
                    return true;
                }

                // Note that these detections are a starting point. See https://www.chromium.org/updates/same-site/incompatible-clients for more detections.

                // Cover all iOS based browsers here. This includes:
                // - Safari on iOS 12 for iPhone, iPod Touch, iPad
                // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
                // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
                // All of which are broken by SameSite=None, because they use the iOS networking stack
                if (userAgent.Contains("CPU iPhone OS 12") || userAgent.Contains("iPad; CPU OS 12"))
                {
                    return true;
                }

                // Cover Mac OS X based browsers that use the Mac OS networking stack. This includes:
                // - Safari on Mac OS X.
                // This does not include:
                // - Chrome on Mac OS X
                // Because they do not use the Mac OS networking stack.
                if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                    userAgent.Contains("Version/") && userAgent.Contains("Safari"))
                {
                    return true;
                }

                // Cover Chrome 50-69, because some versions are broken by SameSite=None,
                // and none in this range require it.
                // Note: this covers some pre-Chromium Edge versions,
                // but pre-Chromium Edge does not require SameSite=None.
                if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
                {
                    return true;
                }

                // Unreal Engine runs Chromium 59, but does not advertise as Chrome until 4.23. Treat versions of Unreal
                // that don't specify their Chrome version as lacking support for SameSite=None.
                if (userAgent.Contains("UnrealEngine") && !userAgent.Contains("Chrome"))
                {
                    return true;
                }

                return false;
            }

            public static bool AllowsSameSiteNone(string userAgent)
            {
                return !DisallowsSameSiteNone(userAgent);
            }


        }
    }
}
