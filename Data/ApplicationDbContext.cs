using databaseApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace databaseApp.Data;
public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }
    public DbSet<Movies> Movie { get; set; }
}