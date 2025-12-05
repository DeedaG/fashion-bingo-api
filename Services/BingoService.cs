using System;
using System.Collections.Generic;
using System.Linq;

public class BingoService
{
    private readonly Random _random = new Random();
    private readonly List<int> _numberPool = Enumerable.Range(1, 75).ToList();
    private readonly Random _rand = new Random();
    private readonly ClosetService _closetService;

    public BingoService(ClosetService closetService)
    {
        _closetService = closetService;
    }

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
        return ClothingCatalog.CreateRandomItem(rarity, _random);
    }

    public ClothingItem GenerateReward(Guid playerId)
    {
        string[] rarities = { "Common", "Rare", "Epic", "Legendary" };
        string rarity = rarities[_random.Next(rarities.Length)];
        HashSet<string>? ownedNames = null;
        try
        {
            var closet = _closetService.GetCloset(playerId);
            if (closet?.Count > 0)
            {
                ownedNames = closet
                    .Where(item => !string.IsNullOrWhiteSpace(item.Name))
                    .Select(item => item.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
            }
        }
        catch
        {
            // If fetching the closet fails, fall back to null which allows the catalog to pick any item.
        }

        return ClothingCatalog.CreateRandomItem(rarity, _random, ownedNames);
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
