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
        // Load from database instead of dictionary
        var items = _context.Player
                            .Where(c => c.Id == playerId)
                            .Where(x => x.Closet != null)
                            .SelectMany(p => p.Closet)
                            .ToList();

        // Map ClosetItem (DB entity) to ClothingItem if needed
        return items.Select(c => new ClothingItem
        {
            Id = c.Id,
            Name = c.Name,
            Type = c.Type
            // Map other properties
        }).ToList();
    }

    public ClothingItem AddItemToCloset(Guid playerId, ClothingItem item)
    {
        var closetItem = new ClothingItem
        {
            Id = Guid.NewGuid(),
            PlayerId = playerId,
            Name = item.Name,
            Type = item.Type
            // Map other properties
        };

        _context.ClothingItem.Add(closetItem);
        _context.SaveChanges();

        return item;
    }

    public Mannequin? EquipItem(Guid playerId, ClothingItem item)
    {
        var player = _context.Player
                            .Where(p => p.Id == playerId)
                             .Include(p => p.CurrentMannequin)
                             .ThenInclude(m => m.EquippedItems)
                             .FirstOrDefault(p => p.Id == playerId);

        if (player != null)
        {
            player.CurrentMannequin.EquippedItems[item.Type] = item;
            _context.SaveChanges();
            return player.CurrentMannequin;
        }

        return null;
    }
}
