using System;

public class MysteryBoxHistoryEntryDto
{
    public Guid RewardId { get; set; }
    public DateTime DateOpened { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public string ItemStyle { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string Rarity { get; set; } = string.Empty;
    public int CoinsAwarded { get; set; }
    public int GemsAwarded { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int CoinsSpent { get; set; }
    public int GemsSpent { get; set; }
}
