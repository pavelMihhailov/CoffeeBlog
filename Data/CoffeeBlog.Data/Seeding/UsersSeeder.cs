namespace CoffeeBlog.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CoffeeBlog.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal class UsersSeeder : ISeeder
    {
        private readonly IConfiguration configuration;

        public UsersSeeder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUsersAsync(userManager);
        }

        private async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var user = await userManager.FindByEmailAsync(this.configuration["Administrator:Email"]);
            if (user == null)
            {
                var result = await userManager.CreateAsync(
                    new ApplicationUser()
                    {
                        UserName = this.configuration["Administrator:Username"],
                        Email = this.configuration["Administrator:Email"],
                        EmailConfirmed = true,
                    },
                    password: this.configuration["Administrator:Password"]);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
