using Microsoft.EntityFrameworkCore;
using Dream.Models;

namespace Dream.Data;

public class DreamDbContext : DbContext
{
    public DreamDbContext(DbContextOptions<DreamDbContext> options)
        : base(options) { }

    public DbSet<Thought> Thoughts { get; set; }
}
