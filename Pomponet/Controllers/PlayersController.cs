using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _playersService;

        public PlayersController(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Players>>> GetAll()
        {
            var players = await _playersService.GetAll();
            var playersNotEliminated = players.Where(p => !p.Deleted).ToList();
            return Ok(await _playersService.GetAll());
        }
        [HttpGet("{Id_Player}")]

        public async Task<ActionResult<Players>> getPlayer(int Id_Player)
        {
            var Player = await _playersService.GetPlayers(Id_Player);
            if (Player == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Player.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Player);
        }

        [HttpPost]
        public async Task<ActionResult<Players>> CreatePlayers(int Score, int Id_Person)
        {
            var Createplayers = await _playersService.CreatePlayers(Score, Id_Person);
            if (Createplayers != null)
            {
                return Ok(Createplayers);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Player}")]
        public async Task<ActionResult<Players>> UpdatePlayers(int Id_Player, int Score, int Id_Person)
        {
            var Updateplayers = await _playersService.UpdatePlayers(Id_Player, Score, Id_Person);
            if (Updateplayers != null)
            {
                return Ok(Updateplayers);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Player}")]
        public async Task<ActionResult<Players>> DeletePlayers(int Id_Player)
        {
            var playersToDelete = await _playersService.DeletePlayers(Id_Player);
            if (playersToDelete != null)
            {
                return Ok(playersToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}