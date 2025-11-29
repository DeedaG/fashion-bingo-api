using System;
public class ClosetService
{
    // In-memory for demo; replace with DB in production
    private readonly Dictionary<Guid, Player> _players;

    public ClosetService(Dictionary<Guid, Player> players)
    {
        _players = players;
    }

    public List<ClothingItem> GetCloset(Guid playerId)
    {
        if (_players.TryGetValue(playerId, out var player))
            return player.Closet;
        return new List<ClothingItem>();
    }

    public ClothingItem AddItemToCloset(Guid playerId, ClothingItem item)
    {
        if (_players.TryGetValue(playerId, out var player))
        {
            player.Closet.Add(item);
            return item;
        }
        return null;
    }

    public Mannequin EquipItem(Guid playerId, ClothingItem item)
    {
        if (_players.TryGetValue(playerId, out var player))
        {
            player.CurrentMannequin.EquippedItems[item.Type] = item;
            return player.CurrentMannequin;
        }
        return null;
    }
}
