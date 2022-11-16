using Microsoft.EntityFrameworkCore;
using Movies.API.Models;

namespace Movies.API.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
}
