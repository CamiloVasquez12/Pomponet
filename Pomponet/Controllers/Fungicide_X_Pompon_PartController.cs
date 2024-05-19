using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class Fungicide_X_Pompon_PartController : ControllerBase
    {
        private readonly IFungicide_X_Pompon_PartService _fungicide_x_pompon_partService;

        public Fungicide_X_Pompon_PartController(IFungicide_X_Pompon_PartService fungicide_x_pompon_partService)
        {
            _fungicide_x_pompon_partService = fungicide_x_pompon_partService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Fungicide_X_Pompon_Part>>> GetAll()
        {
            var fungicide_x_pompon_part = await _fungicide_x_pompon_partService.GetAll();
            var fungicide_x_pompon_partNotEliminated = fungicide_x_pompon_part.Where(f => !f.Deleted).ToList();
            return Ok(await _fungicide_x_pompon_partService.GetAll());
        }
        [HttpGet("{Id_Fungicide_X_Pompon_Part}")]

        public async Task<ActionResult<Fungicide_X_Pompon_Part>> getFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part)
        {
            var Fungicide_X_Pompon_Par = await _fungicide_x_pompon_partService.GetFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part);
            if (Fungicide_X_Pompon_Par == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Fungicide_X_Pompon_Par.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Fungicide_X_Pompon_Par);
        }

        [HttpPost]
        public async Task<ActionResult<Fungicide_X_Pompon_Part>> CreateFungicide_X_Pompon_Part(int Id_Pompon_Part, int Id_Fungicide)
        {
            var Createfungicide_x_pompon_part = await _fungicide_x_pompon_partService.CreateFungicide_X_Pompon_Part(Id_Pompon_Part, Id_Fungicide);
            if (Createfungicide_x_pompon_part != null)
            {
                return Ok(Createfungicide_x_pompon_part);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Fungicide_X_Pompon_Part}")]
        public async Task<ActionResult<Fungicide_X_Pompon_Part>> UpdateFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part, int Id_Pompon_Part, int Id_Fungicide)
        {
            var Updatfungicide_x_pompon_part = await _fungicide_x_pompon_partService.UpdateFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part, Id_Pompon_Part, Id_Fungicide);
            if (Updatfungicide_x_pompon_part != null)
            {
                return Ok(Updatfungicide_x_pompon_part);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Fungicide_X_Pompon_Part}")]
        public async Task<ActionResult<Fungicide_X_Pompon_Part>> DeleteFungicide_X_Pompon_Part(int Id_Fungicide_X_Pompon_Part)
        {
            var fungicide_x_pompon_partToDelete = await _fungicide_x_pompon_partService.DeleteFungicide_X_Pompon_Part(Id_Fungicide_X_Pompon_Part);
            if (fungicide_x_pompon_partToDelete!= null)
            {
                return Ok(fungicide_x_pompon_partToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }


}