using Microsoft.EntityFrameworkCore;

namespace Portfolio.Server.Data;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options) {
    public DbSet<Project> Projects => Set<Project>();
}