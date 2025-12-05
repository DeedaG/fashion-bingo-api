using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ClosetService
{
    private readonly AppDbContext _context;

    public ClosetService(AppDbContext context)
    {
        _context = context;
    }

    // Keep the same method names as before
    public List<ClothingItem> GetCloset(Guid playerId)
    {
        var player = EnsurePlayer(playerId);
        return player.Closet?.Select(c => new ClothingItem
        {
            Id = c.Id,
            PlayerId = playerId,
            Name = c.Name,
            Type = c.Type,
            Rarity = c.Rarity,
            Style = c.Style,
            ImageUrl = c.ImageUrl,
            PrimaryColor = c.PrimaryColor
        }).ToList() ?? new List<ClothingItem>();
    }

    public ClothingItem AddItemToCloset(Guid playerId, ClothingItem item)
    {
        var player = EnsurePlayer(playerId);

        if (player.Closet != null && player.Closet
                .Any(c => string.Equals(c.Name, item.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return player.Closet.First(c => string.Equals(c.Name, item.Name, StringComparison.OrdinalIgnoreCase));
        }

        var closetItem = new ClothingItem
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            Name = item.Name,
            Type = item.Type,
            Rarity = item.Rarity,
            Style = item.Style,
            ImageUrl = item.ImageUrl,
            PrimaryColor = item.PrimaryColor
        };

        player.Closet ??= new List<ClothingItem>();
        player.Closet.Add(closetItem);
        _context.ClothingItem.Add(closetItem);
        _context.SaveChanges();

        return closetItem;
    }

    public Mannequin? EquipItem(Guid playerId, ClothingItem item)
    {
        var player = EnsurePlayer(playerId);

        player.CurrentMannequin ??= new Mannequin();
        player.CurrentMannequin.EquippedItems[item.Type] = item;
        _context.SaveChanges();
        return player.CurrentMannequin;
    }

    public bool ConsumeClosetItems(Guid playerId, IEnumerable<Guid> itemIds)
    {
        var ids = itemIds?
            .Where(id => id != Guid.Empty)
            .Distinct()
            .ToList() ?? new List<Guid>();

        if (ids.Count == 0)
        {
            return false;
        }

        var player = EnsurePlayer(playerId);
        player.Closet ??= new List<ClothingItem>();

        var idSet = new HashSet<Guid>(ids);

        if (player.Closet.Count > 0)
        {
            player.Closet.RemoveAll(item => idSet.Contains(item.Id));
        }

        var itemsToRemove = _context.ClothingItem
            .Where(ci => ci.PlayerId == playerId && idSet.Contains(ci.Id))
            .ToList();

        if (itemsToRemove.Count == 0)
        {
            return false;
        }

        _context.ClothingItem.RemoveRange(itemsToRemove);
        _context.SaveChanges();
        return true;
    }

    private Player EnsurePlayer(Guid playerId)
    {
        var player = _context.Player
                             .Include(p => p.Closet)
                             .Include(p => p.CurrentMannequin)
                             .FirstOrDefault(p => p.Id == playerId);

        if (player == null)
        {
            player = new Player
            {
                Id = playerId,
                Name = $"Player {playerId.ToString()[..8]}",
                Closet = new List<ClothingItem>(),
                CurrentMannequin = new Mannequin()
            };
            _context.Player.Add(player);
            _context.SaveChanges();
        }
        else
        {
            player.Closet ??= new List<ClothingItem>();
            player.CurrentMannequin ??= new Mannequin();
        }

        return player;
    }
}
