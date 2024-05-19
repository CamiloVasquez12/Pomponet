using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class Player_AchievementsController : ControllerBase
    {
        private readonly IPlayer_AchievementsService _player_achievementsService;

        public Player_AchievementsController(IPlayer_AchievementsService player_achievementsService)
        {
            _player_achievementsService = player_achievementsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player_Achievements>>> GetAll()
        {
            var player_achievements = await _player_achievementsService.GetAll();
            var player_achievementsNotEliminated = player_achievements.Where(p => !p.Deleted).ToList();
            return Ok(await _player_achievementsService.GetAll());
        }
        [HttpGet("{Id_Player_Achievement}")]

        public async Task<ActionResult<Player_Achievements>> getPlayer_Achievement(int Id_Player_Achievement)
        {
            var Player_Achievement = await _player_achievementsService.GetPlayer_Achievements(Id_Player_Achievement);
            if (Player_Achievement == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Player_Achievement.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Player_Achievement);
        }

        [HttpPost]
        public async Task<ActionResult<Player_Achievements>> CreatePlayer_Achievements(int Id_Achievement, int Logros_Totales, int Id_Player)
        {
            var Createplayer_achievements = await _player_achievementsService.CreatePlayer_Achievements(Id_Achievement, Logros_Totales, Id_Player);
            if (Createplayer_achievements != null)
            {
                return Ok(Createplayer_achievements);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Player_Achievement}")]
        public async Task<ActionResult<Player_Achievements>> UpdatePlayer_Achievements(int Id_Player_Achievement, int Id_Achievement, int Logros_Totales, int Id_Player)
        {
            var Updateplayer_achievements = await _player_achievementsService.UpdatePlayer_Achievements(Id_Player_Achievement, Id_Achievement, Logros_Totales, Id_Player);
            if (Updateplayer_achievements != null)
            {
                return Ok(Updateplayer_achievements);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Player_Achievement}")]
        public async Task<ActionResult<Player_Achievements>> DeletePlayer_Achievements(int Id_Player_Achievement)
        {
            var player_achievementsToDelete = await _player_achievementsService.DeletePlayer_Achievements(Id_Player_Achievement);
            if (player_achievementsToDelete!= null)
            {
                return Ok(player_achievementsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}