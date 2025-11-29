using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/mysterybox")]
public class MysteryBoxController : ControllerBase
{
    private readonly MysteryBoxService _mysteryBoxService;
    private readonly Dictionary<Guid, Player> _players;

    public MysteryBoxController(
        MysteryBoxService mysteryBoxService,
        Dictionary<Guid, Player> players)
    {
        _mysteryBoxService = mysteryBoxService;
        _players = players;
    }

    [HttpPost("{playerId}/open")]
    public ActionResult<MysteryBoxReward> OpenBox(Guid playerId)
    {
        var player = _players[playerId];
        var reward = _mysteryBoxService.OpenMysteryBox();

        // Add clothing to closet
        player.ClosetItems.Add(reward.Clothing);

        // Add coins/gems
        player.Economy.Coins += reward.Coins;
        player.Economy.Gems += reward.Gems;

        // Save reward history
        player.InventoryRewards.Add(reward);

        return Ok(reward);
    }
}
