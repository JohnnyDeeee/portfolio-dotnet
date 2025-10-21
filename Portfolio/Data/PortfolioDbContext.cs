using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Portfolio.Data;

public class PortfolioDbContext : IdentityDbContext {
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
    
    public DbSet<Project> Projects => Set<Project>();
}