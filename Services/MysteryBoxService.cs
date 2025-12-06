using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class MysteryBoxService
{
    private const int CoinCost = 300;
    private const int GemCost = 1;

    private readonly Random _rng = new();
    private readonly AppDbContext _context;
    private readonly ClosetService _closetService;

    public MysteryBoxService(AppDbContext context, ClosetService closetService)
    {
        _context = context;
        _closetService = closetService;
    }

    public MysteryBoxOpenResult OpenMysteryBox(Guid playerId, string? paymentMethod = null)
    {
        var normalizedMethod = NormalizePaymentMethod(paymentMethod);

        var player = _context.Player
                             .Include(p => p.Economy)
                             .Include(p => p.InventoryRewards)
                                 .ThenInclude(r => r.Clothing)
                             .Include(p => p.Closet)
                             .FirstOrDefault(p => p.Id == playerId);

        if (player == null)
        {
            throw new InvalidOperationException("Player not found.");
        }

        player.Economy ??= new Economy();
        player.InventoryRewards ??= new List<MysteryBoxReward>();

        var coinsSpent = 0;
        var gemsSpent = 0;

        if (normalizedMethod == "gems")
        {
            if (player.Economy.Gems < GemCost)
            {
                throw new InvalidOperationException("Not enough gems to open a mystery box.");
            }

            player.Economy.Gems -= GemCost;
            gemsSpent = GemCost;
        }
        else
        {
            if (player.Economy.Coins < CoinCost)
            {
                throw new InvalidOperationException("Not enough coins to open a mystery box.");
            }

            player.Economy.Coins -= CoinCost;
            coinsSpent = CoinCost;
        }

        var rarity = RollRarity();
        var excludedNames = player.Closet?
            .Select(item => item.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var prototype = ClothingCatalog.CreateRandomItem(rarity, _rng, excludedNames);
        var closetItem = _closetService.AddItemToCloset(playerId, prototype);

        var bonusCoins = _rng.Next(50, 301);
        var bonusGems = rarity switch
        {
            "Legendary" => 2,
            "Epic" => 1,
            _ => 0
        };

        player.Economy.Coins += bonusCoins;
        player.Economy.Gems += bonusGems;

        var rewardRecord = new MysteryBoxReward
        {
            PlayerId = playerId,
            ClothingId = closetItem.Id,
            Clothing = closetItem,
            Coins = bonusCoins,
            Gems = bonusGems,
            Rarity = rarity,
            PaymentMethod = normalizedMethod,
            CoinsSpent = coinsSpent,
            GemsSpent = gemsSpent,
            DateOpened = DateTime.UtcNow
        };

        player.InventoryRewards.Add(rewardRecord);
        _context.MysteryBoxReward.Add(rewardRecord);
        _context.SaveChanges();

        return new MysteryBoxOpenResult
        {
            Item = closetItem,
            Rarity = rarity,
            BonusCoins = bonusCoins,
            BonusGems = bonusGems,
            Economy = player.Economy,
            PaymentMethod = normalizedMethod,
            CoinsSpent = coinsSpent,
            GemsSpent = gemsSpent
        };
    }

    public MysteryBoxHistoryResponseDto GetHistory(Guid playerId)
    {
        var player = _context.Player
                             .Include(p => p.InventoryRewards)
                                 .ThenInclude(r => r.Clothing)
                             .FirstOrDefault(p => p.Id == playerId);

        if (player == null)
        {
            throw new InvalidOperationException("Player not found.");
        }

        player.InventoryRewards ??= new List<MysteryBoxReward>();

        var ordered = player.InventoryRewards
                            .OrderByDescending(r => r.DateOpened)
                            .ToList();

        var entries = ordered
            .Select(r => new MysteryBoxHistoryEntryDto
            {
                RewardId = r.Id,
                DateOpened = r.DateOpened,
                ItemName = r.Clothing?.Name ?? string.Empty,
                ItemType = r.Clothing?.Type ?? string.Empty,
                ItemStyle = r.Clothing?.Style ?? string.Empty,
                ImageUrl = r.Clothing?.ImageUrl,
                Rarity = r.Rarity,
                CoinsAwarded = r.Coins,
                GemsAwarded = r.Gems,
                PaymentMethod = r.PaymentMethod,
                CoinsSpent = r.CoinsSpent,
                GemsSpent = r.GemsSpent
            })
            .ToList();

        var coinSources = new CoinSourceBreakdownDto
        {
            CoinsFromMysteryBoxes = ordered.Sum(r => r.Coins),
            CoinsSpentOnMysteryBoxes = ordered.Sum(r => r.CoinsSpent)
        };

        return new MysteryBoxHistoryResponseDto
        {
            Entries = entries,
            CoinSources = coinSources
        };
    }

    public MysteryBoxPricingDto GetPricing()
    {
        return new MysteryBoxPricingDto
        {
            CoinCost = CoinCost,
            GemCost = GemCost
        };
    }

    private string NormalizePaymentMethod(string? paymentMethod)
    {
        if (string.Equals(paymentMethod, "gems", StringComparison.OrdinalIgnoreCase))
        {
            return "gems";
        }

        return "coins";
    }

    private string RollRarity()
    {
        var rarityRoll = _rng.Next(1, 101);
        return rarityRoll switch
        {
            <= 60 => "Common",
            <= 85 => "Rare",
            <= 95 => "Epic",
            _ => "Legendary"
        };
    }
}
