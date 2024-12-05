using ContactBookApi.Data.Contexts;
using ContactBookApi.Domain.Constants;
using ContactBookApi.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBookApi.Infrastructure.Seed;

public class Seeder
{
    public static async Task Run(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope()
            .ServiceProvider.GetRequiredService<AppDbContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
            await context.Database.MigrateAsync();

        if (!context.Roles.Any())
        {
            var roles = new List<IdentityRole>
            {
                new() { Name = Roles.Admin, NormalizedName = Roles.Admin.ToUpper() },
                new() { Name = Roles.User, NormalizedName = Roles.User.ToUpper() }
            };
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();

            var userManager = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = new User("Admin", "admin@admin.com");

            await userManager.CreateAsync(user, "Admin@123");
            await userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}