using System;
using Microsoft.EntityFrameworkCore;

public class MysteryBoxReward
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public Guid ClothingId { get; set; }
    public Player Player { get; set; } = null!;
    public ClothingItem Clothing { get; set; } = null!;
    public int Coins { get; set; }
    public int Gems { get; set; }
    public string Rarity { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = "coins";
    public int CoinsSpent { get; set; }
    public int GemsSpent { get; set; }
    public DateTime DateOpened { get; set; } = DateTime.UtcNow;
}
