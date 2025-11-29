using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/leaderboard")]
public class LeaderboardController : ControllerBase
{
    private readonly LeaderboardService _leaderboardService;
    private readonly List<string> _requiredTypes = new List<string> { "Shirt", "Pants", "Shoes", "Hat", "Accessory" };

    public LeaderboardController(LeaderboardService leaderboardService)
    {
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public ActionResult<List<Player>> GetLeaderboard()
    {
        return Ok(_leaderboardService.GetLeaderboard(_requiredTypes));
    }

    [HttpGet("{playerId}/check")]
    public ActionResult<bool> IsFullyDressed(Guid playerId)
    {
        // Check if a specific player is fully dressed
        var player = _leaderboardService.GetLeaderboard(_requiredTypes)
            .FirstOrDefault(p => p.Id == playerId);
        return Ok(player != null);
    }
}
