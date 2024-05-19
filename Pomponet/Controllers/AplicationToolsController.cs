using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class AplicationToolsController : ControllerBase
    {
        private readonly IAplicationToolsService _aplicationtoolsService;

        public AplicationToolsController(IAplicationToolsService aplicationtoolsService)
        {
            _aplicationtoolsService = aplicationtoolsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AplicationTools>>> GetAll()
        {
            var aplicationtools = await _aplicationtoolsService.GetAll();
            var aplicationtoolsNotEliminated = aplicationtools.Where(a => !a.Deleted).ToList();
            return Ok(aplicationtoolsNotEliminated);
        }
        [HttpGet("{Id_AplicationTool}")]

        public async Task<ActionResult<AplicationTools>> getAplicationTools(int Id_AplicationTool)
        {
            var AplicationToo = await _aplicationtoolsService.GetAplicationTools(Id_AplicationTool);
            if (AplicationToo == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (AplicationToo.Deleted == true)
            {
                return BadRequest("Registro no valido");
            }
            return Ok(AplicationToo);
        }

        [HttpPost]
        public async Task<ActionResult<AplicationTools>> CreateAplicationTools(string Tool, string Quantity, string Description, int Price)
        {
            var Createaplicationtools = await _aplicationtoolsService.CreateAplicationTools(Tool, Quantity, Description, Price);
            if (Createaplicationtools != null)
            {
                return Ok(Createaplicationtools);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_AplicationTool}")]
        public async Task<ActionResult<AplicationTools>> UpdateAplicationTools(int Id_AplicationTool, string Tool, string Quantity, string Description, int Price)
        {
            var Updateaplicationtools = await _aplicationtoolsService.UpdateAplicationTools(Id_AplicationTool, Tool, Quantity, Description, Price);
            if (Updateaplicationtools != null)
            {
                return Ok(Updateaplicationtools);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_AplicationTool}")]
        public async Task<ActionResult<AplicationTools>> DeleteAplicationTools(int Id_AplicationTool)
        {
            var aplicationtoolsToDelete = await _aplicationtoolsService.DeleteAplicationTools(Id_AplicationTool);
            if (aplicationtoolsToDelete != null)
            {
                return Ok(aplicationtoolsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}