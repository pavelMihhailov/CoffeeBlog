namespace CoffeeBlog.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Common;
    using CoffeeBlog.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class UserRoleSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public UserRoleSeeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserRoleAsync(
                dbContext,
                roleManager,
                userManager,
                this.configuration["Administrator:Email"],
                GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserRoleAsync(
            ApplicationDbContext dbContext,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            string username,
            string roleName)
        {
            var user = await userManager.FindByEmailAsync(username);
            var role = await roleManager.FindByNameAsync(roleName);

            bool exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = role.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
