using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/mysterybox")]
public class MysteryBoxController : ControllerBase
{
    private readonly MysteryBoxService _mysteryBoxService;
    private readonly PlayerService _playerService;
    private readonly ClosetService _closetService;

    public MysteryBoxController(
        MysteryBoxService mysteryBoxService,
        PlayerService playerService,
        ClosetService closetService)
    {
        _mysteryBoxService = mysteryBoxService;
        _playerService = playerService;
        _closetService = closetService;
    }

    [HttpPost("{playerId}/open")]
    public ActionResult<MysteryBoxReward> OpenBox(Guid playerId)
    {
        var reward = _mysteryBoxService.OpenMysteryBox();

        var player = _playerService.GetPlayer(playerId);
        player.Economy.Coins += reward.Coins;
        player.Economy.Gems += reward.Gems;
        player.InventoryRewards.Add(reward);
        _closetService.AddItemToCloset(playerId, reward.Clothing);

        return Ok(reward);
    }
}
