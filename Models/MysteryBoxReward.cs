using Microsoft.EntityFrameworkCore;

public class MysteryBoxReward
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    public ClothingItem Clothing { get; set; } = null!;
    public int Coins { get; set; }
    public int Gems { get; set; }
    public string Rarity { get; set; } = string.Empty;
}
