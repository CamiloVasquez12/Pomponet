using Microsoft.AspNetCore.Mvc;
using Pomponet.Model;
using Pomponet.Services;

namespace Pomponet.Controllers
{
    [Route("api[Controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsService _sensorsService;

        public SensorsController(ISensorsService sensorsService)
        {
            _sensorsService = sensorsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sensors>>> GetAll()
        {
            var sensors = await _sensorsService.GetAll();
            var sensorsNotEliminated = sensors.Where(s => !s.Deleted).ToList();
            return Ok(await _sensorsService.GetAll());
        }
        [HttpGet("{Id_Sensor}")]

        public async Task<ActionResult<Sensors>> getSensor(int Id_Sensor)
        {
            var Sensor = await _sensorsService.GetSensors(Id_Sensor);
            if (Sensor == null)
            {
                return BadRequest("Registro no encontrado");
            }
            if (Sensor.Deleted == true)
            {
                return BadRequest("Regsitro no valido");
            }
            return Ok(Sensor);
        }

        [HttpPost]
        public async Task<ActionResult<Sensors>> CreateSensors(string Sensor, int Price, string Description, int Id_crop)
        {
            var Createsensors = await _sensorsService.CreateSensors(Sensor, Price, Description, Id_crop);
            if (Createsensors != null)
            {
                return Ok(Createsensors);
            }
            else
            {
                return BadRequest("Error al insertar dato");
            }
        }

        [HttpPut("{Id_Sensor}")]
        public async Task<ActionResult<Sensors>> UpdateSensors(int Id_Sensor, string Sensor, int Price, string Description, int Id_crop)
        {
            var Updatesensors = await _sensorsService.UpdateSensors(Id_Sensor, Sensor, Price, Description, Id_crop);
            if (Updatesensors != null)
            {
                return Ok(Updatesensors);
            }
            else
            {
                return BadRequest("Error en la base de datos");
            }
        }
        [HttpDelete("Delete/{Id_Sensor}")]
        public async Task<ActionResult<Sensors>> DeleteSensors(int Id_Sensor)
        {
            var sensorsToDelete = await _sensorsService.DeleteSensors(Id_Sensor);
            if (sensorsToDelete != null)
            {
                return Ok(sensorsToDelete);
            }
            else
            {
                return BadRequest("Error al actualizar la base de datos :(");
            }
        }
    }
}