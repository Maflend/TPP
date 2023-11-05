using Microsoft.EntityFrameworkCore;
using TPP.Api.Domain;

namespace TPP.Api.EfCore;

public class TppDbContext : DbContext
{
    public TppDbContext() : base() { }

    public TppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
