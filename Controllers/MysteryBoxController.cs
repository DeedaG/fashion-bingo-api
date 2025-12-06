using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/mysterybox")]
public class MysteryBoxController : ControllerBase
{
    private readonly MysteryBoxService _mysteryBoxService;

    public MysteryBoxController(MysteryBoxService mysteryBoxService)
    {
        _mysteryBoxService = mysteryBoxService;
    }

    [HttpGet("pricing")]
    public ActionResult<MysteryBoxPricingDto> GetPricing()
    {
        return Ok(_mysteryBoxService.GetPricing());
    }

    [HttpPost("{playerId}/open")]
    public ActionResult<MysteryBoxOpenResult> OpenBox(Guid playerId, [FromBody] OpenMysteryBoxRequest? request)
    {
        try
        {
            var result = _mysteryBoxService.OpenMysteryBox(playerId, request?.PaymentMethod);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{playerId}/history")]
    public ActionResult<MysteryBoxHistoryResponseDto> GetHistory(Guid playerId)
    {
        try
        {
            var response = _mysteryBoxService.GetHistory(playerId);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
