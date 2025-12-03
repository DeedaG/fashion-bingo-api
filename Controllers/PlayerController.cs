using System;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/player")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet("{playerId}")]
    public ActionResult<List<Player>> GetPlayer(Guid playerId)
    {
        return Ok(_playerService.GetPlayer(playerId));
    }

     [HttpPost("createPlayer")]
    public ActionResult<Player> CreatePlayer([FromBody] CreatePlayerRequest request)
    {
        if (request == null || request.PlayerId == Guid.Empty)
        {
            return BadRequest("playerId is required.");
        }

        if (_playerService.PlayerExists(request.PlayerId))
        {
            return Conflict("Player already exists.");
        }

        var player = new Player
        {
            Id = request.PlayerId,
            Name = string.IsNullOrWhiteSpace(request.Name) ? $"Player {request.PlayerId.ToString()[..8]}" : request.Name,
            Closet = new List<ClothingItem>()
        };
        _playerService.AddPlayer(player);
        return Ok(player);
    }

}
