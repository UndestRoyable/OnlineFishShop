using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineFishShop.Data;
using OnlineFishShop.Data.Import.Helpers;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Web.WebServices;

namespace OnlineFishShop.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<OnlineFishShopDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //            services.AddDefaultIdentity<ApplicationUser>()
            //                .AddEntityFrameworkStores<OnlineFishShopDbContext>();
            //
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 2;

                    //user settings
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<OnlineFishShopDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ZvezdichkaCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                // Requires `using Microsoft.AspNetCore.Authentication.Cookies;`
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            //Configure app secrets
//            services.Configure<AppKeyConfig>(this.Configuration.GetSection("AppKeys"));

            //Add application services.
            services.AddTransient<IEmailSender, CustomEmailSender>();

//            services.AddTransient<IHtmlSanitizer, HtmlSanitizer>();
//            services.AddTransient<IHtmlService, HtmlService>();

//            services.AddSingleton<IShoppingCartManager, ShoppingCartManager>();

            services.AddSession();

            //Add data services.
            //services.AddDataServices();

            //Add external login options
//            services.AddAuthentication().AddFacebook(facebookOptions =>
//            {
//                facebookOptions.AppId = this.Configuration.GetSection("AppKeys")["FacebookAppId"];
//                facebookOptions.AppSecret = this.Configuration.GetSection("AppKeys")["FacebookAppSecret"];
//            });

            //services.AddAutoMapper();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddMvc(options => { options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //scope seed db
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<OnlineFishShopDbContext>();
                context.Database.Migrate();

                CreateRoles(serviceProvider).Wait();
                context.EnsureSeedData();
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }

            //Here you could create a super user who will maintain the web app
            var username = this.Configuration.GetSection("UserSettings")["AdminUsername"];
            var email = this.Configuration.GetSection("UserSettings")["AdminEmail"];

            var superUser = new ApplicationUser
            {
                UserName = username,
                Email = email
            };

            //Ensure you have these values in your appsettings.json or secrets.json file
            //TODO Stoyan Lupov 23-12-2018 Currently in user secrets. Must inform the team about this since its outside the project tree
            var userPwd = this.Configuration.GetSection("UserSettings")["AdminPassword"];
            var user = await userManager.FindByNameAsync(
                this.Configuration.GetSection("UserSettings")["AdminUsername"]);

            if (user == null)
            {
                var createSuperUser = await userManager.CreateAsync(superUser, userPwd);
                if (createSuperUser.Succeeded)
                    await userManager.AddToRoleAsync(superUser, "Admin");
            }
        }
    }
}