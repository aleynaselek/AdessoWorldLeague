using Domain;
using Microsoft.EntityFrameworkCore; 

public class AdessoWorldLeagueDbContext : DbContext
{
    public AdessoWorldLeagueDbContext(DbContextOptions<AdessoWorldLeagueDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupTeam> GroupTeams { get; set; }
    public DbSet<Draw> Draws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdessoWorldLeagueDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
