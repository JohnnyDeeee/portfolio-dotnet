using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data;

public static class IdentitySeeder {
    public static async Task SeedAdminAsync(
        WebApplication app
    ) {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        PortfolioDbContext db = services.GetRequiredService<PortfolioDbContext>();
        await db.Database.EnsureCreatedAsync();

        UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        IConfiguration config = services.GetRequiredService<IConfiguration>();

        string adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole)) {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        string username = config["Admin:Username"]!;
        string password = config["Admin:Password"]!;

        IdentityUser? user = await userManager.FindByNameAsync(username);
        if (user == null) {
            user = new IdentityUser {
                UserName = username,
                Email = $"{username}@local"
            };

            await userManager.CreateAsync(user, password);
        }

        // Ensure the user is in Admin role
        if (!await userManager.IsInRoleAsync(user, adminRole)) {
            await userManager.AddToRoleAsync(user, adminRole);
        }
    }
}