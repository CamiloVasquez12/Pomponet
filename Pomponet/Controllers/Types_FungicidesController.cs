using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class Types_FungicidesController : ControllerBase
    {
        private readonly ITypes_FungicidesService _types_fungicidesService;

        public Types_FungicidesController(ITypes_FungicidesService types_fungicidesService)
        {
            _types_fungicidesService = types_fungicidesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Types_Fungicides>>> GetAll()
        {
            var types_fungicides = await _types_fungicidesService.GetAll();
            var types_fungicidesNotEliminated = types_fungicides.Where(t => !t.Deleted).ToList();
            return Ok(await _types_fungicidesService.GetAll());
        }
        [HttpGet("{Id_Type_Fungicide}")]

        public async Task<ActionResult<Types_Fungicides>> getType_Fungicide(int Id_Type_Fungicide)
        {
            var Type_Fungicide = await _types_fungicidesService.GetTypes_Fungicides(Id_Type_Fungicide);
            if (Type_Fungicide == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Type_Fungicide.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Type_Fungicide);
        }

        [HttpPost]
        public async Task<ActionResult<Types_Fungicides>> CreateTypes_Fungicides(string Type_Fungicide, int Id_Funicide)
        {
            var Createtypes_fungicides = await _types_fungicidesService.CreateTypes_Fungicides(Type_Fungicide, Id_Funicide);
            if (Createtypes_fungicides != null)
            {
                return Ok(Createtypes_fungicides);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Type_Fungicide}")]
        public async Task<ActionResult<Types_Fungicides>> UpdateTypes_Fungicides(int Id_Type_Fungicide, string Type_Fungicide, int Id_Funicide)
        {
            var Updatetypes_fungicides = await _types_fungicidesService.UpdateTypes_Fungicides(Id_Type_Fungicide, Type_Fungicide, Id_Funicide);
            if (Updatetypes_fungicides != null)
            {
                return Ok(Updatetypes_fungicides);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Type_Fungicide}")]
        public async Task<ActionResult<Types_Fungicides>> DeleteTypes_Fungicides(int Id_Type_Fungicide)
        {
            var types_fungicidesToDelete = await _types_fungicidesService.DeleteTypes_Fungicides(Id_Type_Fungicide);
            if (types_fungicidesToDelete!= null)
            {
                return Ok(types_fungicidesToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        } 
    }
}