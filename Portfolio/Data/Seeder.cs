using Microsoft.EntityFrameworkCore;

namespace Portfolio.Server.Data;

public static class Seeder {
    public static async Task SeedInitialProjects(
        WebApplication webApplication
    ) {
        using IServiceScope scope = webApplication.Services.CreateScope();
        PortfolioDbContext db = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
        await db.Database.EnsureCreatedAsync();
        if (!await db.Projects.AnyAsync()) {
            db.Projects.AddRange(
                new Project {
                    Title = "Portfolio Website", Description = "A personal portfolio built with Blazor and MudBlazor."
                },
                new Project {
                    Title = "Task Tracker", Description = "A simple task tracking app using EF Core and SQLite."
                },
                new Project { Title = "Blog Engine", Description = "Minimal blog platform with server-side rendering." }
            );
            await db.SaveChangesAsync();
        }
    }
}