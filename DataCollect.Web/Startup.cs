using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataCollect.Web.Data;
using DataCollect.Web.Services;
using DataCollect.Model;
using DataCollect.Service.Service;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using DataCollect.Web.Utities.ViewLocationExpanders;
using DataCollect.Web.Services.Action;
using DataCollect.Web.Services.Service;
using Microsoft.AspNetCore.Authorization;

namespace DataCollect.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("admin"));
            });

            services.AddMvc()
                .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/Pages/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Pages/{0}.cshtml");
            }).AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                    options.Conventions.AuthorizeFolder("/Event", "RequireAdmin");
                    options.Conventions.AuthorizeFolder("/Books", "RequireAdmin");
                });

            services.AddTransient<EventService>();
            services.AddTransient<RowService>();
            services.AddTransient<ExportAction>();
            services.AddTransient<ImportAction>();
            services.AddTransient<SheetService>();
            services.AddTransient<ColumnService>();
            services.AddTransient<BookService>();
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
