public class ClothingItem
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // Shirt, Pants, Shoes, etc.
    public string Rarity { get; set; } = string.Empty; // Common, Rare, Epic, Legendary
    public string Style { get; set; } = string.Empty; // Casual, Formal, Sporty
    public string? ImageUrl { get; set; }
}
