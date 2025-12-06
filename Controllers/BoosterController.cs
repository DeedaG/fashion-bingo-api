using Microsoft.AspNetCore.Mvc;
using System;

[ApiController]
[Route("api/boosters")]
public class BoosterController : ControllerBase
{
    private readonly BoosterService _boosterService;

    public BoosterController(BoosterService boosterService)
    {
        _boosterService = boosterService;
    }

    [HttpGet("{playerId}")]
    public ActionResult<BoosterInventoryDto> GetInventory(Guid playerId)
    {
        try
        {
            return Ok(_boosterService.GetInventory(playerId));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{playerId}/purchase")]
    public ActionResult<BoosterInventoryDto> Purchase(Guid playerId, [FromBody] PurchaseBoosterRequest request)
    {
        try
        {
            var inventory = _boosterService.PurchaseBooster(playerId, request?.BoosterType);
            return Ok(inventory);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{playerId}/consume")]
    public ActionResult<BoosterInventoryDto> Consume(Guid playerId, [FromBody] ConsumeBoosterRequest request)
    {
        try
        {
            var inventory = _boosterService.ConsumeBooster(playerId, request?.BoosterType);
            return Ok(inventory);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
