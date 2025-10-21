using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data;

public class PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : DbContext(options) {
    public DbSet<Project> Projects => Set<Project>();
}