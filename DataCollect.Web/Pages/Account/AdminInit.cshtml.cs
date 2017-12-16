using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCollect.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DataCollect.Web.Pages.Account
{
    public class AdminInitModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext context;

        public AdminInitModel([FromServices]RoleManager<IdentityRole> roleManager,
            [FromServices]UserManager<ApplicationUser> userManager,
            [FromServices]ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await CreateRoleIfNotExistAsync("admin");
            await CreateRoleIfNotExistAsync("user");

            var user =await userManager.FindByNameAsync("admin");
            if (user == null)
            {
                await userManager.CreateAsync(new ApplicationUser { UserName = "admin" }, "admin");
                user = await userManager.FindByNameAsync("admin");
            }else if (await userManager.CheckPasswordAsync(user, "admin"))
            {
                await userManager.RemovePasswordAsync(user);
                await userManager.AddPasswordAsync(user,"admin");
            }

            if (await userManager.IsInRoleAsync(user, "admin"))
            {
                await userManager.AddToRoleAsync(user, "admin");
            }

            return Page();
        }

        public async Task CreateRoleIfNotExistAsync(string name)
        {
            if (!await roleManager.RoleExistsAsync(name))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
            }
        }
    }
}