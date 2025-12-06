using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/shop")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;

    public ShopController(ShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpGet("offers")]
    public ActionResult<IEnumerable<ShopOffer>> GetOffers()
    {
        return Ok(_shopService.GetOffers());
    }

    [HttpPost("{playerId}/buy-item")]
    public ActionResult<ShopPurchaseResult> BuyItem(Guid playerId, [FromBody] ShopPurchaseRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.OfferId))
        {
            return BadRequest("offerId is required.");
        }

        try
        {
            var result = _shopService.PurchaseOffer(playerId, request.OfferId);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
