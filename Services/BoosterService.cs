using System;
using Microsoft.EntityFrameworkCore;

public class BoosterService
{
    private const int FreeDaubCoinCost = 150;
    private const int AutoDaubGemCost = 1;

    private readonly AppDbContext _context;

    public BoosterService(AppDbContext context)
    {
        _context = context;
    }

    public BoosterInventoryDto GetInventory(Guid playerId)
    {
        var player = LoadPlayer(playerId);
        return BuildInventory(player);
    }

    public BoosterInventoryDto PurchaseBooster(Guid playerId, string? boosterType)
    {
        var player = LoadPlayer(playerId);
        boosterType ??= string.Empty;

        switch (boosterType.ToLowerInvariant())
        {
            case "free-daub":
                EnsureCoins(player, FreeDaubCoinCost);
                player.Economy.Coins -= FreeDaubCoinCost;
                player.FreeDaubTokens += 1;
                break;
            case "auto-daub":
                EnsureGems(player, AutoDaubGemCost);
                player.Economy.Gems -= AutoDaubGemCost;
                player.AutoDaubBoosts += 1;
                break;
            default:
                throw new InvalidOperationException("Booster type not recognized.");
        }

        _context.SaveChanges();
        return BuildInventory(player);
    }

    public BoosterInventoryDto ConsumeBooster(Guid playerId, string? boosterType)
    {
        var player = LoadPlayer(playerId);
        boosterType ??= string.Empty;
        switch (boosterType.ToLowerInvariant())
        {
            case "free-daub":
                if (player.FreeDaubTokens <= 0)
                {
                    throw new InvalidOperationException("No Free Daub tokens available.");
                }
                player.FreeDaubTokens -= 1;
                break;
            case "auto-daub":
                if (player.AutoDaubBoosts <= 0)
                {
                    throw new InvalidOperationException("No Auto Daub boosts available.");
                }
                player.AutoDaubBoosts -= 1;
                break;
            default:
                throw new InvalidOperationException("Booster type not recognized.");
        }

        _context.SaveChanges();
        return BuildInventory(player);
    }

    private Player LoadPlayer(Guid playerId)
    {
        var player = _context.Player
                             .Include(p => p.Economy)
                             .FirstOrDefault(p => p.Id == playerId);
        if (player == null)
        {
            throw new InvalidOperationException("Player not found.");
        }
        player.Economy ??= new Economy();
        return player;
    }

    private static void EnsureCoins(Player player, int required)
    {
        if (player.Economy.Coins < required)
        {
            throw new InvalidOperationException("Not enough coins for this booster.");
        }
    }

    private static void EnsureGems(Player player, int required)
    {
        if (player.Economy.Gems < required)
        {
            throw new InvalidOperationException("Not enough gems for this booster.");
        }
    }

    private static BoosterInventoryDto BuildInventory(Player player)
    {
        return new BoosterInventoryDto
        {
            FreeDaubTokens = player.FreeDaubTokens,
            AutoDaubBoosts = player.AutoDaubBoosts,
            Economy = player.Economy
        };
    }
}
