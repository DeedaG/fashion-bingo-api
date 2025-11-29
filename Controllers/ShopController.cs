using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/shop")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;
    private readonly Dictionary<Guid, Player> _players;

    public ShopController(
        ShopService shopService,
        Dictionary<Guid, Player> players)
    {
        _shopService = shopService;
        _players = players;
    }



[HttpPost("{playerId}/buy")]
public ActionResult<Economy> Buy(Guid playerId, [FromBody] ShopPurchase purchase)
{
    var player = _players[playerId];

    switch (purchase.Type)
    {
        case "coins":
            player.Economy.Gems -= purchase.Cost;
            player.Economy.Coins += purchase.Amount;
            break;

        case "gems":
            // TODO: integrate real payments later
            player.Economy.Gems += purchase.Amount;
            break;

        case "mysterybox":
            if (player.Economy.Coins < purchase.Cost)
                return BadRequest("Not enough coins");
            player.Economy.Coins -= purchase.Cost;
            break;
    }

    return Ok(player.Economy);
}
}

