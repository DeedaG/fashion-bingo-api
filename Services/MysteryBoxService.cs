public class MysteryBoxService
{
    private readonly Random _rng = new();

    public MysteryBoxReward OpenMysteryBox()
    {
        // Weighted rarity
        var rarityRoll = _rng.Next(1, 101);
        string rarity = rarityRoll switch
        {
            <= 60 => "Common",
            <= 85 => "Rare",
            <= 95 => "Epic",
            _ => "Legendary"
        };

        // Generate clothing item
        var clothing = new ClothingItem
        {
            Id = Guid.NewGuid(),
            Name = $"{rarity} Item {Guid.NewGuid().ToString().Substring(0, 5)}",
            Type = GetRandomType(),
            ImageUrl = "/assets/clothes/default.png",
            Rarity = rarity
        };

        return new MysteryBoxReward
        {
            Clothing = clothing,
            Coins = _rng.Next(50, 300),
            Gems = rarity == "Legendary" ? 5 : 0,
            Rarity = rarity
        };
    }

    private string GetRandomType()
    {
        var types = new[] { "Shirt", "Pants", "Shoes", "Hat", "Accessory" };
        return types[_rng.Next(types.Length)];
    }
}
