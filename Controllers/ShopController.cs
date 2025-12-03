using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/shop")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;
    private readonly PlayerService _playerService;

    public ShopController(
        ShopService shopService,
        PlayerService playerService)
    {
        _shopService = shopService;
        _playerService = playerService;
    }



[HttpPost("{playerId}/buy")]
public ActionResult<Economy> Buy(Guid playerId, [FromBody] ShopPurchase purchase)
{
    var player = _playerService.GetPlayer(playerId);

    switch (purchase.Type)
    {
        case "coins":
            player.Economy.Gems -= purchase.Cost;
            player.Economy.Coins += purchase.Amount;
            break;

        case "gems":
            player.Economy.Gems += purchase.Amount;
            break;

        case "mysterybox":
            if (player.Economy.Coins < purchase.Cost)
                return BadRequest("Not enough coins");
            player.Economy.Coins -= purchase.Cost;
            break;
    }

    _shopService.SaveEconomy(player);
    return Ok(player.Economy);
}
}
