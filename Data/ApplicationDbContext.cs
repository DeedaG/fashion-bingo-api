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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Economy>().HasKey(e => e.Id);
        modelBuilder.Entity<Mannequin>().HasKey(m => m.Id);
        modelBuilder.Entity<MysteryBoxReward>().HasKey(r => r.Id);
        modelBuilder.Entity<MysteryBoxReward>().HasOne(r => r.Player)
                                            .WithMany(p => p.InventoryRewards)
                                            .HasForeignKey(r => r.PlayerId)
                                            .OnDelete(DeleteBehavior.Restrict);
                                    
    }
}
