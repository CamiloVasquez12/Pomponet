using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementsService _achievementsService;

        public AchievementsController(IAchievementsService achievementsService)
        {
            _achievementsService = achievementsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Achievements>>> GetAll()
        {
            var achievements = await _achievementsService.GetAll();
            var achievementsNotEliminated = achievements.Where(a => !a.Deleted).ToList();
            return Ok(achievementsNotEliminated);
        }
        [HttpGet("{Id_Achievement}")]

        public async Task<ActionResult<Achievements>> getAchievements(int Id_Achievement)
        {
            var Achievement = await _achievementsService.GetAchievements(Id_Achievement);
            if (Achievement == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Achievement.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Achievement);
        }

        [HttpPost]
        public async Task<ActionResult<Achievements>> CreateAchievements(string Achievement)
        {
            var Createachievements = await _achievementsService.CreateAchievements(Achievement);
            if (Createachievements != null)
            {
                return Ok(Createachievements);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Achievement}")]
        public async Task<ActionResult<Achievements>> UpdateAchievements(int Id_Achievement, string Achievement)
        {
            var Updateachievements = await _achievementsService.UpdateAchievements(Id_Achievement, Achievement);
            if (Updateachievements != null)
            {
                return Ok(Updateachievements);
            }
            else 
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Achievement}")]
        public async Task<ActionResult<Achievements>> DeleteAchievements(int Id_Achievement)
        {
            var achievementsToDelete = await _achievementsService.DeleteAchievements(Id_Achievement);

            if (achievementsToDelete != null)
            {
                return Ok(achievementsToDelete);
            }
            else 
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }


}