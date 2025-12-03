using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
    public ActionResult<int[][]> GetNewCard()
    {
        return Ok(_bingoService.GenerateCard());
    }


    [HttpPost("claimreward")]
    public ActionResult<ClothingItem> ClaimReward([FromBody] JsonElement body)
    {
        try
        {
            // Accept either { "playerId": "..." } or { "PlayerId": "..." }
            string? playerIdStr = null;
            if (body.ValueKind != JsonValueKind.Object)
            {
                Console.WriteLine("[BingoController] claimreward: invalid JSON body (not an object)");
                return BadRequest("Expected JSON object with playerId.");
            }

            if (body.TryGetProperty("playerId", out var pidProp) && pidProp.ValueKind == JsonValueKind.String)
            {
                playerIdStr = pidProp.GetString();
            }
            else if (body.TryGetProperty("PlayerId", out var pidProp2) && pidProp2.ValueKind == JsonValueKind.String)
            {
                playerIdStr = pidProp2.GetString();
            }

            if (string.IsNullOrWhiteSpace(playerIdStr) || !Guid.TryParse(playerIdStr, out var playerId))
            {
                Console.WriteLine($"[BingoController] claimreward: invalid or missing playerId in body: {body}");
                return BadRequest("Invalid or missing playerId GUID.");
            }

            Console.WriteLine($"[BingoController] claimreward: request for playerId: {playerId}");

            // For demo, generate a reward (could be tied to playerId later)
            ClothingItem reward = _bingoService.GenerateReward();
            // In real app, fetch player and add item to closet, persist, etc.
            return Ok(reward);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[BingoController] claimreward: exception parsing body: {ex}");
            return BadRequest("Failed to parse request body.");
        }
    }

    [HttpGet("next-number")]
    public IActionResult GetNextNumber()
    {
        int number = _bingoService.GetNextNumberForCaller();
        return Ok(number);
    }

    [HttpGet("reveal-next")]
    public IActionResult RevealNextNumbers()
    {
        var numbers = _bingoService.PeekNextNumbers(3);
        return Ok(numbers);
    }
}
