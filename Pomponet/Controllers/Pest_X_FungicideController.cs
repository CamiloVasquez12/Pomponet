using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class Pest_X_FungicideController : ControllerBase
    {
        private readonly IPest_X_FungicideService _pest_x_fungicideService;

        public Pest_X_FungicideController(IPest_X_FungicideService pest_x_fungicideService)
        {
            _pest_x_fungicideService = pest_x_fungicideService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pest_X_Fungicide>>> GetAll()
        {
            var pest_x_fungicide = await _pest_x_fungicideService.GetAll();
            var pest_x_fungicideNotEliminated = pest_x_fungicide.Where(p => !p.Deleted).ToList();
            return Ok(await _pest_x_fungicideService.GetAll());
        }
        [HttpGet("{Id_Pest_X_Fungicide}")]

        public async Task<ActionResult<Pest_X_Fungicide>> getPest_X_Fungicid(int Id_Pest_X_Fungicide)
        {
            var Pest_X_Fungicid = await _pest_x_fungicideService.GetPest_X_Fungicide(Id_Pest_X_Fungicide);
            if (Pest_X_Fungicid == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Pest_X_Fungicid.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Pest_X_Fungicid);
        }

        [HttpPost]
        public async Task<ActionResult<Pest_X_Fungicide>> CreatePest_X_Fungicide(int Id_Pest, int Id_Fungicide)
        {
            var Createpest_x_fungicide = await _pest_x_fungicideService.CreatePest_X_Fungicide(Id_Pest, Id_Fungicide);
            if (Createpest_x_fungicide != null)
            {
                return Ok(Createpest_x_fungicide);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Pest_X_Fungicide}")]
        public async Task<ActionResult<Pest_X_Fungicide>> UpdatePest_X_Fungicide(int Id_Pest_X_Fungicide, int Id_Pest, int Id_Fungicide)
        {
            var Updatepest_x_fungicide = await _pest_x_fungicideService.UpdatePest_X_Fungicide(Id_Pest_X_Fungicide, Id_Pest, Id_Fungicide);
            if (Updatepest_x_fungicide != null)
            {
                return Ok(Updatepest_x_fungicide);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Pest_X_Fungicide}")]
        public async Task<ActionResult<Pest_X_Fungicide>> DeletePest_X_Fungicide(int Id_Pest_X_Fungicide)
        {
            var pest_x_fungicideToDelete = await _pest_x_fungicideService.DeletePest_X_Fungicide(Id_Pest_X_Fungicide);
            if (pest_x_fungicideToDelete!= null)
            {
                return Ok(pest_x_fungicideToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}