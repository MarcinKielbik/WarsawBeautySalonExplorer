using Microsoft.EntityFrameworkCore;
using WarsawBeautySalonExplorer.Api.Models;

namespace WarsawBeautySalonExplorer.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Salon> Salons => Set<Salon>();
}