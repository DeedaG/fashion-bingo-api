public class BingoService
{
    private readonly Random _random = new Random();
    private readonly List<int> _numberPool = Enumerable.Range(1, 75).ToList();
    private readonly Random _rand = new Random();

    // Generate a new bingo card (5x5)
    public int[][] GenerateCard()
    {
        int[][] card = new int[5][]; // 5 rows

        for (int row = 0; row < 5; row++)
        {
            card[row] = new int[5]; // 5 columns
        }

        for (int col = 0; col < 5; col++)
        {
            // Generate 5 random numbers for this column
            List<int> numbers = Enumerable.Range(col * 15 + 1, 15)
                                        .OrderBy(x => _random.Next())
                                        .Take(5)
                                        .ToList();

            for (int row = 0; row < 5; row++)
            {
                card[row][col] = numbers[row];
            }
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

    public int GetNextNumberForCaller()
    {
        if (_numberPool.Count == 0)
            return -1;

        int index = _rand.Next(_numberPool.Count);
        int number = _numberPool[index];
        _numberPool.RemoveAt(index);
        return number;
    }

    public List<int> PeekNextNumbers(int count)
    {
        return _numberPool.Take(count).ToList();
    }
   


}
