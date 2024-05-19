using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class Pompon_PartsController : ControllerBase
    {
        private readonly IPompon_PartsService _pompon_partsService;

        public Pompon_PartsController(IPompon_PartsService pompon_partsService)
        {
            _pompon_partsService = pompon_partsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pompon_Parts>>> GetAll()
        {
            var pompon_parts = await _pompon_partsService.GetAll();
            var pompon_partsNotEliminated = pompon_parts.Where(p => !p.Deleted).ToList();
            return Ok(await _pompon_partsService.GetAll());
        }
        [HttpGet("{Id_Pompon_Part}")]

        public async Task<ActionResult<Pompon_Parts>> getPompon_Part(int Id_Pompon_Part)
        {
            var Pompon_Part = await _pompon_partsService.GetPompon_Parts(Id_Pompon_Part);
            if (Pompon_Part == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Pompon_Part.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Pompon_Part);
        }

        [HttpPost]
        public async Task<ActionResult<Pompon_Parts>> CreatePompon_Parts(string Part)
        {
            var Createpompon_parts = await _pompon_partsService.CreatePompon_Parts(Part);
            if (Createpompon_parts!= null)
            {
                return Ok(Createpompon_parts);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Pompon_Part}")]
        public async Task<ActionResult<Pompon_Parts>> UpdatePompon_Parts(int Id_Pompon_Part, string Part)
        {
            var Updatepompon_parts = await _pompon_partsService.UpdatePompon_Parts(Id_Pompon_Part, Part);
            if (Updatepompon_parts!= null)
            {
                return Ok(Updatepompon_parts);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Pompon_Part}")]
        public async Task<ActionResult<Pompon_Parts>> DeletePompon_Parts(int Id_Pompon_Part)
        {
            var pompon_partsToDelete = await _pompon_partsService.DeletePompon_Parts(Id_Pompon_Part);
            if (pompon_partsToDelete != null)
            {
                return Ok(pompon_partsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}