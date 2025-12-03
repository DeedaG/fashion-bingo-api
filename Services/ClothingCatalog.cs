using System;
using System.Collections.Generic;
using System.Linq;

public static class ClothingCatalog
{
    private static readonly List<ClothingTemplate> Templates = new()
    {
        new ClothingTemplate(
            "Denim Daydream Jacket",
            "Shirt",
            "Streetwear",
            "Common",
            "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Midnight Run Joggers",
            "Pants",
            "Athleisure",
            "Common",
            "https://images.unsplash.com/photo-1521572162788-99c7d89882ff?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Sunrise Canvas Bucket Hat",
            "Hat",
            "Casual",
            "Common",
            "https://images.unsplash.com/photo-1441984904996-e0b6ba687e04?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Luna Lace Top",
            "Shirt",
            "Boho",
            "Rare",
            "https://images.unsplash.com/photo-1503341455253-b2e723bb3dbb?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Prism Runner Sneakers",
            "Shoes",
            "Athleisure",
            "Rare",
            "https://images.unsplash.com/photo-1514996937319-344454492b37?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Saffron Silk Wrap Pants",
            "Pants",
            "Resort",
            "Rare",
            "https://images.unsplash.com/photo-1521572165474-6864f9cf17ac?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Velvet Ember Blazer",
            "Shirt",
            "Evening",
            "Epic",
            "https://images.unsplash.com/photo-1475180098004-ca77a66827be?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Obsidian Moto Boots",
            "Shoes",
            "Streetwear",
            "Epic",
            "https://images.unsplash.com/photo-1549298916-f52d724204b4?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Halo Mirage Visor",
            "Accessory",
            "Futuristic",
            "Epic",
            "https://images.unsplash.com/photo-1503341455253-b2e723bb3dbc?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Crystal Cascade Earrings",
            "Accessory",
            "Runway",
            "Legendary",
            "https://images.unsplash.com/photo-1524504388940-b1c1722653e1?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Gilded Skyline Heels",
            "Shoes",
            "Runway",
            "Legendary",
            "https://images.unsplash.com/photo-1518544889280-37f4ca38e4b4?auto=format&fit=crop&w=600&q=80"),
        new ClothingTemplate(
            "Aurora Feathered Fascinator",
            "Hat",
            "Couture",
            "Legendary",
            "https://images.unsplash.com/photo-1505685296765-3a2736de412f?auto=format&fit=crop&w=600&q=80")
    };

    public static ClothingItem CreateRandomItem(string rarity, Random rng)
    {
        var pool = Templates
            .Where(t => string.Equals(t.Rarity, rarity, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (pool.Count == 0)
        {
            pool = Templates;
        }

        var template = pool[rng.Next(pool.Count)];
        return new ClothingItem
        {
            Id = Guid.NewGuid(),
            Name = template.Name,
            Type = template.Type,
            Style = template.Style,
            Rarity = template.Rarity,
            ImageUrl = template.ImageUrl
        };
    }

    private record ClothingTemplate(
        string Name,
        string Type,
        string Style,
        string Rarity,
        string ImageUrl);
}
