using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;
using Portfolio.Components;
using Portfolio.Data;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add EF Core with SQLite (file-based)
string connectionString = builder.Configuration.GetConnectionString("PortfolioDb") ?? "Data Source=portfolio.db";
builder.Services.AddDbContext<PortfolioDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PortfolioDbContext>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudMarkdownServices();

// Configure Identity application cookie to use /login path
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/";
});

// Support HttpContext access and auth service
builder.Services.AddHttpContextAccessor();

WebApplication app = builder.Build();

// Ensure database exists and seed some data
await Seeder.SeedInitialProjects(app);
await IdentitySeeder.SeedAdminAsync(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Must be above app.UseAntiforgery();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapPost("/auth/login", async (HttpContext httpContext, SignInManager<IdentityUser> signInManager) => {
    var form = await httpContext.Request.ReadFormAsync();
    string username = form["Username"].ToString();
    string password = form["Password"].ToString();
    string returnUrl = form["ReturnUrl"].ToString();
    if (string.IsNullOrWhiteSpace(returnUrl)) returnUrl = "/";

    var result = await signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: true);
    if (result.Succeeded) {
        return Results.Redirect(returnUrl);
    }
    return Results.Redirect("/login?error=1");
});

app.MapPost("/auth/logout", async (SignInManager<IdentityUser> signInManager) => {
    await signInManager.SignOutAsync();
    return Results.Redirect("/");
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();