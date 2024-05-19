using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private readonly ICropsService _cropsService;

        public CropsController(ICropsService cropsService)
        {
            _cropsService = cropsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Crops>>> GetAll()
        {
            var crops = await _cropsService.GetAll();
            var cropsNotEliminated = crops.Where(c => !c.Deleted).ToList();
            return Ok(await _cropsService.GetAll());
        }
        [HttpGet("{Id_Crop}")]

        public async Task<ActionResult<Crops>> GetCrops(int Id_Crop)
        {
            var Crop = await _cropsService.GetCrops(Id_Crop);
            if (Crop == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Crop.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Crop);
        }

        [HttpPost]
        public async Task<ActionResult<Crops>> CreateCrops(int Crop_Number, int Id_Player)
        {
            var Createcrops = await _cropsService.CreateCrops(Crop_Number, Id_Player);
            if (Createcrops != null)
            {
                return Ok(Createcrops);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Crop}")]
        public async Task<ActionResult<Crops>> UpdateCrops(int Id_Crop, int Crop_Number, int Id_Player)
        {
            var Updateacrops = await _cropsService.UpdateCrops(Id_Crop, Crop_Number, Id_Player);
            if (Updateacrops != null)
            {
                return Ok(Updateacrops);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Crop}")]
        public async Task<ActionResult<Crops>> DeleteCrops(int Id_Crop)
        {
            var cropsToDelete = await _cropsService.DeleteCrops(Id_Crop);

            if (cropsToDelete != null)
            {
                return Ok(cropsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }


}