using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/bingo")]
public class BingoController : ControllerBase
{
    private readonly BingoService _bingoService;

    public BingoController(BingoService bingoService)
    {
        _bingoService = bingoService;
    }

    [HttpGet("newcard")]
    public ActionResult<int[,]> GetNewCard()
    {
        return Ok(_bingoService.GenerateCard());
    }

    [HttpPost("claimreward")]
    public ActionResult<ClothingItem> ClaimReward([FromBody] Guid playerId)
    {
        // For demo, generate a reward
        ClothingItem reward = _bingoService.GenerateReward();
        
        // In real app, fetch player and add item to closet
        // player.Closet.Add(reward);
        return Ok(reward);
    }
}
