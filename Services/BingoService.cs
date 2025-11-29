public class BingoService
{
    private readonly Random _random = new Random();

    // Generate a new bingo card (5x5)
    public int[,] GenerateCard()
    {
        int[,] card = new int[5,5];
        for (int col = 0; col < 5; col++)
        {
            List<int> numbers = Enumerable.Range(col * 15 + 1, 15).OrderBy(x => _random.Next()).Take(5).ToList();
            for (int row = 0; row < 5; row++)
                card[row, col] = numbers[row];
        }
        return card;
    }

    // Generate a random clothing reward
    public ClothingItem GenerateReward()
    {
        string[] rarities = { "Common", "Rare", "Epic", "Legendary" };
        string rarity = rarities[_random.Next(rarities.Length)];
        string[] types = { "Shirt", "Pants", "Shoes", "Hat", "Accessory" };
        string type = types[_random.Next(types.Length)];
        return new ClothingItem
        {
            Id = Guid.NewGuid(),
            Name = $"{rarity} {type}",
            Rarity = rarity,
            Type = type,
            Style = "Casual"
        };
    }
}
