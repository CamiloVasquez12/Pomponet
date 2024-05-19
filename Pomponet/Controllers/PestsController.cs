using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class PestsController : ControllerBase
    {
        private readonly IPestsService _pestsService;

        public PestsController(IPestsService pestsService)
        {
            _pestsService = pestsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pests>>> GetAll()
        {
            var pests = await _pestsService.GetAll();
            var pestsNotEliminated = pests.Where(p => !p.Deleted).ToList();
            return Ok(await _pestsService.GetAll());
        }
        [HttpGet("{Id_Pest}")]

        public async Task<ActionResult<Pests>> getPest(int Id_Pest)
        {
            var Pest = await _pestsService.GetPests(Id_Pest);
            if (Pest == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Pest.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Pest);
        }

        [HttpPost]
        public async Task<ActionResult<Pests>> CreatePests(string Pest)
        {
            var Createpests = await _pestsService.CreatePests(Pest);
            if (Createpests != null)
            {
                return Ok(Createpests);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Pest}")]
        public async Task<ActionResult<Pests>> UpdatePests(int Id_Pest, string Pest)
        {
            var Updatepests = await _pestsService.UpdatePests(Id_Pest, Pest);
            if (Updatepests != null)
            {
                return Ok(Updatepests);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Pest}")]
        public async Task<ActionResult<Pests>> DeletePests(int Id_Pest)
        {
            var pestsToDelete = await _pestsService.DeletePests(Id_Pest);
            if (pestsToDelete != null)
            {
                return Ok(pestsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}