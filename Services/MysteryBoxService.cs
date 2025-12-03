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

        var clothing = ClothingCatalog.CreateRandomItem(rarity, _rng);

        return new MysteryBoxReward
        {
            Clothing = clothing,
            Coins = _rng.Next(50, 300),
            Gems = rarity == "Legendary" ? 5 : 0,
            Rarity = rarity
        };
    }
}
