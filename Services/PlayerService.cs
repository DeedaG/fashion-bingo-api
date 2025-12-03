using Microsoft.EntityFrameworkCore;

public class PlayerService
{
    private readonly AppDbContext _context;

    public PlayerService(AppDbContext context)
    {
        _context = context;
    }

    public Player GetPlayer(Guid playerId)
    {
        // Load from database instead of dictionary
        var player = _context.Player
                            .Where(c => c.Id == playerId)
                            .FirstOrDefault();

        if(player == null)
        {
            throw new Exception("Player not found");
        }                    

        return player;
    }

    public bool PlayerExists(Guid playerId)
    {
        return _context.Player.Any(p => p.Id == playerId);
    }

    public void AddPlayer(Player player)
    {
        _context.Player.Add(player);
        _context.SaveChanges();
    }
}
