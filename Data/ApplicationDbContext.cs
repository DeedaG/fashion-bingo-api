using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<ClothingItem> ClothingItem { get; set; }
    public DbSet<Player> Player { get; set; }
    public DbSet<Economy> Economy { get; set; }
    public DbSet<Mannequin> Mannequin { get; set; }
    public DbSet<MysteryBoxReward> MysteryBoxReward { get; set; }
    public DbSet<User> User { get; set; }

    public DbSet<Reward> Reward { get; set; }
}
