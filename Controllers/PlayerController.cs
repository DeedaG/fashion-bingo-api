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
    public ActionResult<Player> CreatePlayer([FromBody] Guid playerId)
    {
        var player = new Player
        {
            Id = playerId,
            Closet = new List<ClothingItem>()
        };
        _playerService.AddPlayer(player);
        return Ok(player);
    }

}
