using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using Portfolio.Server.Components;
using Portfolio.Server.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add EF Core with SQLite (file-based)
string connectionString = builder.Configuration.GetConnectionString("PortfolioDb") ?? "Data Source=portfolio.db";
builder.Services.AddDbContext<PortfolioDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudMarkdownServices();

WebApplication app = builder.Build();

// Ensure database exists and seed some data
await Seeder.SeedInitialProjects(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();