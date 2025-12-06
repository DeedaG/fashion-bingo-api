public class MysteryBoxOpenResult
{
    public ClothingItem Item { get; set; } = null!;
    public string Rarity { get; set; } = string.Empty;
    public int BonusCoins { get; set; }
    public int BonusGems { get; set; }
    public Economy Economy { get; set; } = null!;
    public string PaymentMethod { get; set; } = "coins";
    public int CoinsSpent { get; set; }
    public int GemsSpent { get; set; }
}
