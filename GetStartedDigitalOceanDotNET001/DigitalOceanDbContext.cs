using Microsoft.EntityFrameworkCore;

namespace GetStartedDigitalOceanDotNET001;

public class DigitalOceanDbContext : DbContext
{
    public DigitalOceanDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();
}
