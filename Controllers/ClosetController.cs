using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/closet")]
public class ClosetController : ControllerBase
{
    private readonly ClosetService _closetService;

    public ClosetController(ClosetService closetService)
    {
        _closetService = closetService;
    }

    [HttpGet("{playerId}")]
    public ActionResult<List<ClothingItem>> GetCloset(Guid playerId)
    {
        Console.WriteLine($"[ClosetController] GET closet for playerId: {playerId}");
        return Ok(_closetService.GetCloset(playerId));
    }

    [HttpPost("{playerId}/add")]
    public ActionResult<ClothingItem> AddItem(Guid playerId, [FromBody] ClothingItem item)
    {
        Console.WriteLine($"[ClosetController] ADD item for playerId: {playerId}, item: {item?.Name ?? "null"}");
        return Ok(_closetService.AddItemToCloset(playerId, item));
    }

    [HttpPost("{playerId}/equip")]
    public ActionResult<Mannequin> EquipItem(Guid playerId, [FromBody] ClothingItem item)
    {
        Console.WriteLine($"[ClosetController] EQUIP item for playerId: {playerId}, item: {item?.Name ?? "null"}");
        return Ok(_closetService.EquipItem(playerId, item));
    }

    [HttpPost("{playerId}/consume")]
    public IActionResult ConsumeCloset(Guid playerId, [FromBody] ConsumeClosetItemsRequest request)
    {
        if (request?.ItemIds == null || request.ItemIds.Count == 0)
        {
            return BadRequest("At least one item id is required to consume closet items.");
        }

        Console.WriteLine($"[ClosetController] CONSUME items for playerId: {playerId}, count: {request.ItemIds.Count}");
        var removed = _closetService.ConsumeClosetItems(playerId, request.ItemIds);

        if (!removed)
        {
            return NotFound("No matching closet items were found for removal.");
        }

        return NoContent();
    }
}
