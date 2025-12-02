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

}
