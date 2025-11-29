public class LeaderboardService
{
    private readonly Dictionary<Guid, Player> _players;

    public LeaderboardService(Dictionary<Guid, Player> players)
    {
        _players = players;
    }

    // Check if mannequin is fully dressed
    public bool IsMannequinFullyDressed(Player player, List<string> requiredTypes)
    {
        return requiredTypes.All(type => player.CurrentMannequin.EquippedItems.ContainsKey(type));
    }

    // Get leaderboard of fully dressed players
    public List<Player> GetLeaderboard(List<string> requiredTypes)
    {
        return _players.Values
            .Where(p => IsMannequinFullyDressed(p, requiredTypes))
            .OrderBy(p => p.Name)
            .ToList();
    }
}
