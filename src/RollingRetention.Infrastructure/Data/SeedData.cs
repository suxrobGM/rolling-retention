using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RollingRetention.Core.Entities;

namespace RollingRetention.Infrastructure.Data
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider service)
        {
            await MigrateDatabaseAsync(service);
            await AddTestUsersAsync(service);
        }

        private static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
        {
            try
            {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private static async Task AddTestUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Populate test users
            for (var i = 1; i <= 10; i++)
            {
                await AddUserAsync($"TestUser{i}", "Password1", userManager);
            }
        }

        private static async Task AddUserAsync(string userName, 
            string password, 
            UserManager<ApplicationUser> userManager)
        {
            var existingUser = await userManager.FindByNameAsync(userName);
            if (existingUser == null)
            {
                await userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = userName,
                    Email = $"{userName}@mail.ru",
                    EmailConfirmed = true

                }, password);
            }
        }
    }
}