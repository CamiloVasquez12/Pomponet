using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class FungicidesController : ControllerBase
    {
        private readonly IFungicidesService _fungicidesService;

        public FungicidesController(IFungicidesService fungicidesService)
        {
            _fungicidesService = fungicidesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fungicides>>> GetAll()
        {
            var fungicides = await _fungicidesService.GetAll();
            var fungicidesNotEliminated = fungicides.Where(f => !f.Deleted).ToList();
            return Ok(await _fungicidesService.GetAll());
        }
        [HttpGet("{Id_Fungicide}")]

        public async Task<ActionResult<Fungicides>> getFungicide(int Id_Fungicide)
        {
            var Fungicide = await _fungicidesService.GetFungicides(Id_Fungicide);
            if (Fungicide == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Fungicide.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Fungicide);
        }

        [HttpPost]
        public async Task<ActionResult<Fungicides>> CreateFungicides(string Name_Fungicide, int Quantity, string Description, int Id_crop)
        {
            var Createfungicides = await _fungicidesService.CreateFungicides(Name_Fungicide, Quantity, Description, Id_crop);
            if (Createfungicides != null)
            {
                return Ok(Createfungicides);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Fungicide}")]
        public async Task<ActionResult<Fungicides>> UpdateFungicides(int Id_Fungicide, string Name_Fungicide, int Quantity, string Description, int Id_crop)
        {
            var Updatefungicides = await _fungicidesService.UpdateFungicides(Id_Fungicide, Name_Fungicide, Quantity, Description, Id_crop);
            if (Updatefungicides != null)
            {
                return Ok(Updatefungicides);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Fungicide}")]
        public async Task<ActionResult<Fungicides>> DeleteFungicides(int Id_Fungicide)
        {
            var fungicidesToDelete = await _fungicidesService.DeleteFungicides(Id_Fungicide);
            if (fungicidesToDelete != null)
            {
                return Ok(fungicidesToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }


}