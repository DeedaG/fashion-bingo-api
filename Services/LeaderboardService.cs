using Microsoft.EntityFrameworkCore;

public class LeaderboardService
{
     private readonly AppDbContext _context;

    public LeaderboardService(AppDbContext context)
    {
        _context = context;
    }

    // Check if mannequin is fully dressed
    public bool IsMannequinFullyDressed(Player player, List<string> requiredTypes)
    {
        return requiredTypes.All(type => player.CurrentMannequin.EquippedItems.ContainsKey(type));
    }

    // Get leaderboard of fully dressed players
    public List<Player> GetLeaderboard(List<string> requiredTypes)
    {
        return _context.Player.Where(p => p.CurrentMannequin != null &&
                                         requiredTypes.All(type => p.CurrentMannequin.EquippedItems.ContainsKey(type)))
                              .OrderBy(p => p.Name)
                              .ToList();
    }
}
