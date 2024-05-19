using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class EppsController : ControllerBase
    {
        private readonly IEppsService _eppsService;

        public EppsController(IEppsService eppsService)
        {
            _eppsService = eppsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Epps>>> GetAll()
        {
            var epps = await _eppsService.GetAll();
            var eppsNotEliminated = epps.Where(e => !e.Deleted).ToList();
            return Ok(await _eppsService.GetAll());
        }
        [HttpGet("{Id_Epp}")]

        public async Task<ActionResult<Epps>> getEpps(int Id_Epp)
        {
            var Epp = await _eppsService.GetEpps(Id_Epp);
            if (Epp == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Epp.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Epp);
        }

        [HttpPost]
        public async Task<ActionResult<Epps>> CreateEpps(string Name_Epp)
        {
            var Createepps = await _eppsService.CreateEpps(Name_Epp);
            if (Createepps != null)
            {
                return Ok(Createepps);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Epp}")]
        public async Task<ActionResult<Epps>> UpdateEpps(int Id_Epp, string Name_Epp)
        {
            var Updateepps = await _eppsService.UpdateEpps(Id_Epp, Name_Epp);
            if (Updateepps != null)
            {
                return Ok(Updateepps);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Epp}")]
        public async Task<ActionResult<Epps>> DeleteEpps(int Id_Epp)
        {
            var eppsToDelete = await _eppsService.DeleteEpps(Id_Epp);

            if (eppsToDelete != null)
            {
                return Ok(eppsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}