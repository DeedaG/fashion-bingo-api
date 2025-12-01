public class ClothingItem
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; } // Shirt, Pants, Shoes, etc.
    public string Rarity { get; set; } // Common, Rare, Epic, Legendary
    public string Style { get; set; } // Casual, Formal, Sporty
    public string ImageUrl { get; set; }
}
