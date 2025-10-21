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
            // Add fake data
            db.Projects.AddRange(
                new Project {
                    Title = "Portfolio Website",
                    Description =
                        "A sleek and responsive portfolio website showcasing my work and skills.\n\n" +
                        "### Key Features\n" +
                        "- Project showcase gallery\n" +
                        "- Dynamic content management\n" +
                        "- Professional material design\n\n" +
                        "### Technical Stack\n" +
                        "Built with cutting-edge Microsoft technologies:\n" +
                        "- Blazor & .NET Core frontend\n" +
                        "- Entity Framework Core for data\n" +
                        "- MudBlazor components\n" +
                        "- ASP.NET Core backend services"
                },
                new Project {
                    Title = "Task Tracker",
                    Description =
                        "A robust application for managing projects and tasks efficiently.\n\n" +
                        "### Core Features\n" +
                        "- Real-time task tracking\n" +
                        "- Priority-based organization\n" +
                        "- Customizable project boards\n" +
                        "- Team collaboration tools\n\n" +
                        "### Technical Highlights\n" +
                        "- SignalR real-time notifications\n" +
                        "- Secure authentication\n" +
                        "- Task dependencies & time tracking\n" +
                        "- Advanced analytics & reporting"
                },
                new Project {
                    Title = "Blog Engine",
                    Description =
                        "A feature-rich content management system for modern blogging needs.\n\n" +
                        "### Core Capabilities\n" +
                        "- Markdown support with live preview\n" +
                        "- Scheduled post publishing\n" +
                        "- SEO optimization tools\n" +
                        "- Multi-user collaboration\n\n" +
                        "### Architecture & Features\n" +
                        "- Microservices-based design\n" +
                        "- Content versioning & backups\n" +
                        "- Category & tag management\n" +
                        "- Integrated analytics\n" +
                        "- Custom theming support\n" +
                        "- Built-in comment system\n" +
                        "- Social media integration"
                }
            );
            await db.SaveChangesAsync();
        }
    }
}